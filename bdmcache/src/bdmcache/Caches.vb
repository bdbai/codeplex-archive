Imports TagLib
Imports System.IO
Imports System.Drawing
Imports System.Collections.ObjectModel
Imports System.Threading
Public Class MusicCache

    Public Shared Sub LoadCaches(ByVal CachePath As String, ByRef Coll As Collection(Of MusicCache))
        Dim cs As New Collection(Of MusicCache)
        Dim _CachePath As String = ""
        If Not CachePath.EndsWith("\") Then _CachePath = CachePath Else _CachePath = CachePath & "\"
        For Each f As String In Directory.GetFiles(_CachePath)
            If f.EndsWith(".dat") Then
                Try
                    Dim cache As New MusicCache(f)
                    Coll.Add(cache)
                Catch ex As Exception

                End Try
            End If
        Next
        For Each d As String In Directory.GetDirectories(_CachePath)
            LoadCaches(d, Coll)
        Next
    End Sub

    Public Property Num As Integer
    Private _FilePath As String
    Public ReadOnly Property FilePath As String
        Get
            Return _FilePath
        End Get
    End Property
    Private IdTag As Id3v2.Tag
    Dim thr As New Thread(AddressOf LoadTag)
    Public Sub New(ByVal CacheFileName As String)
        Dim ava = CheckAvailable(CacheFileName)
        If Not IsNothing(ava) Then
            Throw New IOException(ava)
            Exit Sub
        End If
        _FilePath = CacheFileName
        thr.Start(CacheFileName)
    End Sub

    Private _State As LoadState
    Public ReadOnly Property State As LoadState
        Get
            Return _State
        End Get
    End Property
    Public Event LoadingFinished(Cache As MusicCache)
    Private Sub LoadTag(ByVal CacheFileName As String)
        _LoadingString = "正在载入……"
        Dim fs As FileStream
        Try
            fs = New FileStream(CacheFileName, FileMode.Open)
            _Size = Math.Round(fs.Length / 1024 / 1024, 2).ToString & " Mb"
        Catch ex As Exception
            _LoadingString = ex.Message
            GoTo ending
        End Try

        fs.Seek(324, SeekOrigin.Begin)
        If fs.ReadByte = Asc("A") Then
            _Completed = True
        Else
            _Completed = False
        End If

        Dim bt(0) As Byte
        Dim bts As New Collection(Of Byte)
        '检查头
        fs.Seek(65536, SeekOrigin.Begin)
        Try
            While (bts.Count < fs.Length - 65536 AndAlso Not DoBytesEnd(fs, "LAME"))
                bts.Add(fs.ReadByte)
            End While
        Catch ex As Exception
            _LoadingString = "无效的缓存文件。"
            GoTo ending
        End Try
        bts.Add(0)
        ReDim bt(bts.Count - 1)
        Dim index As Integer = 0
        For Each b As Byte In bts
            bt(index) = bts(index)
            index += 1
        Next

        fs.Dispose()

        Try
            Dim bv As New ByteVector(bt)

            IdTag = New Id3v2.Tag(bv)
        Catch ex As Exception
            _LoadingString = ex.Message
            GoTo ending
        End Try
        _State = LoadState.Finish
        _LoadingString = "加载完成。"
        RaiseEvent LoadingFinished(Me)
        Exit Sub
ending:
        _State = LoadState.ErrorOcc
        Debug.WriteLine(_LoadingString)
        RaiseEvent LoadingFinished(Me)
    End Sub
    Private Function DoBytesEnd(ByRef stream As Stream, ByVal Ending As String) As Boolean
        Dim index As Integer = 0
        For Each ch As Char In Ending.ToCharArray
            index += 1
            If Not Asc(ch) = stream.ReadByte Then
                stream.Seek(-index, SeekOrigin.Current)
                Return False
                Exit Function
            End If
        Next
        stream.Seek(-index, SeekOrigin.Current)
        Return True
    End Function

    Private _LoadingString As String
    Public ReadOnly Property LoadingString As String
        Get
            Return _LoadingString
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            If State = LoadState.Finish Then
                Return IdTag.Title
            Else
                Return _LoadingString
            End If
        End Get
    End Property
    Public ReadOnly Property Artist As String
        Get
            If State = LoadState.Finish Then
                Return IdTag.JoinedPerformers
            Else
                Return _LoadingString
            End If
        End Get
    End Property
    Public ReadOnly Property Album As String
        Get
            If State = LoadState.Finish Then
                Return IdTag.Album
            Else
                Return _LoadingString
            End If
        End Get
    End Property
    Public ReadOnly Property Genre As String
        Get
            If State = LoadState.Finish Then
                Try
                    Return IdTag.Genres(0)
                Catch
                    Return ""
                End Try
            Else
                Return _LoadingString
            End If
        End Get
    End Property
    Public ReadOnly Property Year As UInteger
        Get
            If State = LoadState.Finish Then
                Return IdTag.Year
            Else
                Return 0
            End If
        End Get
    End Property
    Public ReadOnly Property Comment As String
        Get
            If State = LoadState.Finish Then
                Return IdTag.Comment
            Else
                Return _LoadingString
            End If
        End Get
    End Property
    Public ReadOnly Property AlbumPicture As Image
        Get
            If State = LoadState.Finish Then
                Dim returningImage As Image
                If IdTag.Pictures.Length > 0 Then
                    Using ms As New MemoryStream(IdTag.Pictures(0).Data.Data)
                        returningImage = Image.FromStream(ms)
                    End Using
                    Return returningImage
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Get
    End Property
    Private _Size As String
    Public ReadOnly Property Size As String
        Get
            If State = LoadState.Finish Then
                Return _Size
            Else
                Return "0"
            End If
        End Get
    End Property
    Private _Completed As Boolean
    Public ReadOnly Property Completed As Boolean
        Get
            Return _Completed
        End Get
    End Property

    Public Sub WriteToFile(ByVal OutPath As String)
        Dim ava = CheckAvailable(FilePath)
        If Not IsNothing(ava) Then
            Throw New IOException(ava)
            Exit Sub
        End If
        If IO.File.Exists(OutPath) Then
                Try
                    IO.File.Delete(OutPath)
                Catch ex As Exception
                    Throw ex
                    Exit Sub
            End Try
        End If
        Dim fs1, fs2 As FileStream
        Try
            fs1 = New FileStream(FilePath, FileMode.Open)
            fs2 = New FileStream(OutPath, FileMode.CreateNew)
            Using fs1
                Using fs2
                    Dim bt(fs1.Length - 65537) As Byte
                    Dim temp(65535) As Byte
                    fs1.Read(temp, 0, 65536)
                    fs1.Read(bt, 0, fs1.Length - 65536)
                    fs2.Write(bt, 0, bt.Length)
                    fs2.Flush()
                    ReDim bt(0) : ReDim temp(0)
                End Using
            End Using
        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try
    End Sub

    Public Shared Function CheckAvailable(ByRef f As String) As String
        If Not IO.File.Exists(f) Then
            Return "缓存文件不存在。"
            Exit Function
        End If
        If Path.GetFileName(f) = "container.dat" Then
            Return "缓存容器。"
            Exit Function
        End If
        If New FileInfo(f).Length < 65536 Then
            Return "无效的缓存文件"
            Exit Function
        End If
        Return Nothing
    End Function
End Class

Public Enum LoadState
    Loading = 0
    Finish = 1
    ErrorOcc = 2
End Enum