Imports MySql.Data.MySqlClient

Public Class Form3

    Public Sub list_column()
        With LV2
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .Columns.Add("Product_Code", 100)
            .Columns.Add("Product_Name", 250)
            .Columns.Add("Product_Price", 100)
            .Columns.Add("Quantity", 100)
            .Columns.Add("SubTotal", 100)
        End With
    End Sub

    Public Sub Form3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call list_column()
    End Sub



    Private Sub Searchbtn_Click(sender As Object, e As EventArgs) Handles Searchbtn.Click
        Dim connString As String = "server=localhost; user id=root; password=; database=productsdb;"
        Dim sqlQuery As String = "SELECT * FROM `productsdata` WHERE Product_Code = @Product_Code"

        Using sqlConn As New MySqlConnection(connString)
            Using sqlComm As New MySqlCommand(sqlQuery, sqlConn)
                sqlComm.Parameters.AddWithValue("@Product_Code", TB1.Text)

                Try
                    sqlConn.Open()
                    Dim sqlReader As MySqlDataReader = sqlComm.ExecuteReader()

                    If sqlReader.Read() Then
                        TB2.Text = sqlReader("Product_Name").ToString()
                        TB3.Text = sqlReader("Product_Price").ToString()
                    Else
                        MessageBox.Show("Product Code doesn't exist, Please try again.")
                    End If
                    sqlReader.Close()
                Catch ex As MySqlException
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Backbtn_Click(sender As Object, e As EventArgs) Handles Backbtn.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Addbtn_Click(sender As Object, e As EventArgs) Handles Addbtn.Click

        Dim temp As Integer
        temp = LV2.Items.Count
        Dim list As New ListViewItem(TB1.Text)
        list.SubItems.Add(TB2.Text)
        list.SubItems.Add(TB3.Text)
        list.SubItems.Add(TB4.Text)
        list.SubItems.Add(TB6.Text)
        LV2.Items.Add(list)

        TB1.Text = Nothing
        TB2.Text = Nothing
        TB3.Text = Nothing
        TB4.Text = Nothing
        TB6.Text = Nothing

        Dim sum As Integer = 0
        Dim column4 As Integer = 4 ' Assume you want to sum values from the third column (index 2)

        For Each item As ListViewItem In LV2.Items
            Dim subItemText As String = item.SubItems(4).Text
            Dim value As Integer
            If Not String.IsNullOrEmpty(subItemText) AndAlso Integer.TryParse(subItemText, value) Then
                sum += value
            End If
        Next

        TB6.Text = sum.ToString()
    End Sub

    Private Sub Removebtn_Click(sender As Object, e As EventArgs) Handles Removebtn.Click
        If LV2.Items.Count = 0 Then
            MsgBox("No items to remove.", MsgBoxStyle.Exclamation, "Remove Error")
            Exit Sub
        Else
            Dim itemCnt, t As Integer
            LV2.FocusedItem.Remove()
            itemCnt = LV2.Items.Count
            t = 1
        End If
    End Sub

    Private Sub Savebtn_Click(sender As Object, e As EventArgs) Handles Savebtn.Click
        Try
            Dim ProductCode As String = TB1.Text.Trim()
            Dim ProductName As String = TB2.Text.Trim()
            Dim ProductPrice As String = TB3.Text.Trim()
            Dim Pro_Quantity As String = TB4.Text.Trim()

            Save("INSERT INTO productsales (ProductCode, ProductName, ProductPrice, Pro_Quantity) VALUES ('" & ProductCode & "', '" & ProductName & "', '" & ProductPrice & "', '" & Pro_Quantity & "')")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub LV2_DoubleClick(sender As Object, e As EventArgs) Handles LV2.DoubleClick
        If Me.LV2.SelectedItems.Count > 0 Then
            Dim lv1 As ListViewItem = Me.LV2.SelectedItems(0)
            TB1.Text = lv1.SubItems(0).Text
            TB2.Text = lv1.SubItems(1).Text
            TB3.Text = lv1.SubItems(2).Text
            TB4.Text = lv1.SubItems(3).Text
            TB6.Text = lv1.SubItems(4).Text
            LV2.FocusedItem.Remove()
        End If
    End Sub
    Private Sub TB4_TextChanged(sender As Object, e As EventArgs) Handles TB4.TextChanged
        TB6.Text = Val(TB4.Text) * Val(TB3.Text)
    End Sub

    Private Sub Calculate_Click(sender As Object, e As EventArgs) Handles Calculate.Click
        Dim totalAmount As Double
        Dim cash As Double
        If Not Double.TryParse(TB6.Text, totalAmount) Then
            MessageBox.Show("Invalid total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If Not Double.TryParse(TB7.Text, cash) Then
            MessageBox.Show("Invalid cash amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim change As Double = cash - totalAmount
        TB8.Text = change.ToString("0.00")
    End Sub
End Class