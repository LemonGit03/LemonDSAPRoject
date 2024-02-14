Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Public Class Form5

    Dim con As New MySqlConnection("server=localhost; user id=root; password=; database=productsdb;")
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim sql As String
    Dim result As Integer

    Private Sub Generate_btn2_Click(sender As Object, e As EventArgs) Handles Generate_btn2.Click
        Try
            con.Open()
            sql = "SELECT * FROM productsales"
            cmd = New MySqlCommand
            With cmd
                .Connection = con
                .CommandText = Sql
                dr = .ExecuteReader
            End With
            LV3.Items.Clear()
            Do While dr.Read = True
                Dim list = LV3.Items.Add(dr(0))
                list.SubItems.Add(dr.Item(1))
                list.SubItems.Add(dr.Item(2))
                list.SubItems.Add(dr.Item(3))
                list.SubItems.Add(dr.Item(4))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Backbtn_Click(sender As Object, e As EventArgs) Handles Backbtn.Click
        Me.Hide()
        Form1.Show()
    End Sub


End Class