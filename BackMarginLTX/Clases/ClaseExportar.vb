Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Public Class ClaseExportar


    'Public Overloads Sub creararchivo(ByVal dgvp As DataGridView, ByVal titulo As String, Optional ByVal NombreReceta As String = "",
    '                                       Optional ByVal inicio As String = "", Optional ByVal fin As String = "")

    Public Overloads Sub creararchivo(ByVal dgvp As DataGridView)

        Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)
        Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing
        worksheet = workbook.Sheets("Hoja1")
        worksheet = workbook.ActiveSheet
        'Aca se agregan las cabeceras de nuestro datagrid.



        worksheet.Cells(1, 3) = "Cuantificación de Recetas"
        worksheet.Rows.Item(1).Font.Bold = 1
        worksheet.Rows.Item(1).Font.size = 16
        worksheet.Rows.Item(1).HorizontalAlignment = 3

        worksheet.Cells(2, 1) = "Nombre de Receta:"
        worksheet.Rows.Item(2).Font.Bold = 1
        '  worksheet.Cells(2, 2) = NombreReceta
        worksheet.Rows.Item(2).HorizontalAlignment = 2


        worksheet.Cells(3, 1) = "Fecha:"
        worksheet.Rows.Item(3).Font.Bold = 1
        ' worksheet.Cells(3, 2) = inicio & " al " & fin


        For i As Integer = 1 To dgvp.Columns.Count
            worksheet.Cells(5, i) = dgvp.Columns(i - 1).HeaderText
        Next

        'Aca se ingresa el detalle recorrera la tabla celda por celda


        For i As Integer = 0 To dgvp.Rows.Count - 1
            For j As Integer = 0 To dgvp.Columns.Count - 1
                worksheet.Cells(i + 6, j + 1) = dgvp.Rows(i).Cells(j).Value.ToString()
            Next
        Next

        'Aca le damos el formato a nuestro excel

        worksheet.Rows.Item(5).Font.Bold = 1
        worksheet.Rows.Item(5).HorizontalAlignment = 3
        worksheet.Columns.AutoFit()

        worksheet.Columns.HorizontalAlignment = 3

        worksheet.Range("B3:C3").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

        worksheet.Range("A6:XFD1048576").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        worksheet.Range("A6:A1048576").ColumnWidth = 20
        worksheet.Range("B6:B1048576").ColumnWidth = 15
        worksheet.Range("C6:C1048576").ColumnWidth = 50
        worksheet.Range("A6:A1048576").NumberFormat = "#############"
        worksheet.Range("D6:D1048576").NumberFormat = "0.00"

        app.Visible = True
        app = Nothing
        workbook = Nothing
        worksheet = Nothing
        FileClose(1)
        GC.Collect()

    End Sub

End Class


