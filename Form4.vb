Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient
Public Class Form4
    Dim con As New MySqlConnection("server=localhost; user id=root; password=; database=productsdb;")
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim sql As String
    Dim result As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form1.Show()
    End Sub
    Private Sub Generate_btn1_Click(sender As Object, e As EventArgs) Handles Generate_btn1.Click
        Try
            con.Open()
            sql = "SELECT * FROM productsdata"
            cmd = New MySqlCommand
            With cmd
                .Connection = con
                .CommandText = sql
                dr = .ExecuteReader
            End With
            LV1.Items.Clear()
            Do While dr.Read = True
                Dim list = LV1.Items.Add(dr(0))
                list.SubItems.Add(dr.Item(1))
                list.SubItems.Add(dr.Item(2))
                list.SubItems.Add(dr.Item(3))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub


End Class