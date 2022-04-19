Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel

Public Class exportacionreportes
    Dim psproveedor As Paragraph
    Dim psdireccion As Paragraph
    Dim psfechainicio As Paragraph
    Dim psnit As Paragraph
    Dim psnombreProveedor As Paragraph
    Dim pstotalEspacio As Paragraph
    Dim pstotalmonto As Paragraph
    Dim pstotalventas As Paragraph
    Dim pstotalcompras As Paragraph
    Dim pstotalpagar As Paragraph
    Dim nombreproveedor As String
    Dim nit As String
    Dim direccion As String
    Dim sfecha As String
    Dim proveedor As String
    Dim fechainicio As String
    Dim hora As String
    Dim totalmonto As String
    Dim totalespacio As String
    Dim totalventa1 As String
    Dim totalcompra As String
    Dim total As String
    Dim totalconvertido As String
    Dim totalQuetzales As String
    'funcion que crea completamente el pdf


    Public Overloads Sub creararchivo(ByVal titulo As String, ByVal nit As String, ByVal fechainicial1 As String, ByVal proveedores As String, ByVal dgvespacios As DataGridView, _
                                      ByVal dgvproductosC As DataGridView, ByVal dgvproductosV As DataGridView, ByVal dgvproductosM As DataGridView, ByVal nombrevendedor As String, ByVal direccion As String, _
                                      ByVal tasacambio As String, ByVal totalEspacioD As String, ByVal totalespacioQ As String, ByVal totalcompras As String, ByVal totalventa As String, ByVal totalMontod As String, _
                                      ByVal totalmontoQ As String, ByVal totalQ As String, ByVal totalD As String, ByVal totalCD As String, ByVal totalcontrato As String, ByVal contrato As String, ByVal dgvtotales As DataGridView)

        'llenamos todos los datos que iran en nuestro encabezado
        proveedor = ""
        fechainicio = ""
        nombreproveedor = ""
        totalconvertido = ""
        totalQuetzales = ""
        proveedor = "Proveedor: " & vbTab & proveedores & ""
        nit = "NIT del Proveedor : " & vbTab & nit & ""
        direccion = "Direccion: " & vbTab & direccion & ""
        fechainicio = "Mes de Cobro:" & vbTab & fechainicial1 & ""
        nombreproveedor = "Nombre del Proveedor: " & vbTab & nombrevendedor & ""
        'totalventa1 = "     Total de ventas:              Q. " & vbTab & totalventa & ""
        'totalcompra = "     Total de Compras:          Q." & vbTab & totalcompras & ""
        'totalespacio = "     Total de Espacios:          Q." & vbTab & totalespacioQ & "              $. " & vbTab & totalEspacioD & ""
        'totalmonto = "     Total por Monto Fijo:      Q." & vbTab & totalmontoQ & "                $. " & vbTab & totalMontod & ""
        'total = "                                                Q. " & vbTab & totalQ & "              $. " & vbTab & totalD & ""
        If totalD > 0 Then

            totalconvertido = " Tasa de cambio: " & vbTab & tasacambio & "   *  $. " & vbTab & totalD & "   =     Q." & vbTab & totalCD & ""
        End If

        totalQuetzales = "  Total a pagar en el contrato=  Q. " & vbTab & totalcontrato & ""
        'definimos la direccion del archivo...
        Dim sdireccion As String = Environ("UserProfile")

        'Dim nombrearchivo As String = sdireccion & "\OneDrive - Unisuper\Documentos\" & titulo & ".pdf"
        Dim nombrearchivo As String = Application.StartupPath + "\Reportes\" & titulo & ".pdf"
        'Le asignamos el tamaño de la hoja que vamos a utilizar...
        Dim docpdf As Document
        docpdf = New Document(PageSize.LETTER, 25, 25, 55, 40)
        Try
            If File.Exists(nombrearchivo) Then

                My.Computer.FileSystem.DeleteFile(nombrearchivo)

            End If

        Catch ex As Exception

            MessageBox.Show("Se produjo el siguiente error:" & vbCrLf & ex.Message.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            Dim file As FileStream = New FileStream(nombrearchivo, FileMode.OpenOrCreate, _
                                                    FileAccess.ReadWrite, FileShare.ReadWrite)

            Dim owriter As PdfWriter = PdfWriter.GetInstance(docpdf, file)

            '***************creo las variables.....

            psnit = New Paragraph(nit, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            psnit.Alignment = Element.ALIGN_LEFT

            psproveedor = New Paragraph(proveedor, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            psproveedor.Alignment = Element.ALIGN_LEFT

            psdireccion = New Paragraph(direccion, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            psdireccion.Alignment = Element.ALIGN_LEFT

            psfechainicio = New Paragraph(fechainicio, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            psfechainicio.Alignment = Element.ALIGN_LEFT

            psnombreProveedor = New Paragraph(nombreproveedor, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            psnombreProveedor.Alignment = Element.ALIGN_LEFT

            pstotalcompras = New Paragraph(totalcompras, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            pstotalcompras.Alignment = Element.ALIGN_RIGHT

            pstotalventas = New Paragraph(totalventa, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            pstotalventas.Alignment = Element.ALIGN_RIGHT

            pstotalmonto = New Paragraph(totalmonto, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            pstotalmonto.Alignment = Element.ALIGN_RIGHT

            pstotalEspacio = New Paragraph(totalespacio, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7.5, iTextSharp.text.Font.BOLD)))
            pstotalEspacio.Alignment = Element.ALIGN_RIGHT

            pstotalpagar = New Paragraph(totalQuetzales, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
            pstotalpagar.Alignment = Element.ALIGN_RIGHT

            Dim oEvents As New PDFPageEvent("UNISUPER S.A." & vbNewLine, contrato)
            owriter.PageEvent = oEvents

            'se abre el archivo
            docpdf.Open()
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("_____________________________________________________________________________________________________________", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("INFORMACIÓN DEL PROVEEDOR", FontFactory.GetFont("Times New Roman", 11, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph(psproveedor))
            docpdf.Add(New Paragraph(psnit))
            docpdf.Add(New Paragraph(psdireccion))
            docpdf.Add(New Paragraph("Tasa de cambio a utilizar: " & vbTab & tasacambio & "", FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                            "))

            If dgvespacios.RowCount > 0 Then
                docpdf.Add(New Paragraph("ALQUILER DE ESPACIOS:", FontFactory.GetFont("Times New Roman", 11, iTextSharp.text.Font.BOLD)))
                docpdf.Add(New Paragraph("                                            "))
                GenerarPDF(docpdf, dgvespacios)
                docpdf.Add(New Paragraph("                                            "))
                'docpdf.Add(New Paragraph(pstotalEspacio))
            End If

            If dgvproductosM.RowCount > 0 Or dgvproductosV.RowCount > 0 Or dgvproductosC.RowCount > 0 Then
                docpdf.Add(New Paragraph("                                            "))
                docpdf.Add(New Paragraph(" RENTA DE ESPACIO COMERCIAL SOBRE COMPRAS DE PRODUCTO", FontFactory.GetFont("Times New Roman", 11, iTextSharp.text.Font.BOLD)))
            End If

            If dgvproductosM.RowCount > 0 Then
                docpdf.Add(New Paragraph("                                            "))
                GenerarPDF(docpdf, dgvproductosM)
                docpdf.Add(New Paragraph("                                            "))
                ' docpdf.Add(New Paragraph(pstotalmonto))
            End If


            If dgvproductosV.RowCount > 0 Then
                'docpdf.Add(New Paragraph("                                             "))
                'docpdf.Add(New Paragraph("ALQUILER DE PRODUCTO QUE FUERON POR VENTAS", FontFactory.GetFont("Times New Roman", 11, iTextSharp.text.Font.BOLD)))
                docpdf.Add(New Paragraph("                                            "))
                GenerarPDF(docpdf, dgvproductosV)
                docpdf.Add(New Paragraph("                                            "))
                '  docpdf.Add(New Paragraph(pstotalventas))
            End If

            If dgvproductosC.RowCount > 0 Then
                'docpdf.Add(New Paragraph("                                             "))
                'docpdf.Add(New Paragraph("ALQUILER DE PRODUCTO QUE FUERON POR COMPRAS", FontFactory.GetFont("Times New Roman", 11, iTextSharp.text.Font.BOLD)))
                docpdf.Add(New Paragraph("                                            "))
                GenerarPDF(docpdf, dgvproductosC)
                docpdf.Add(New Paragraph("                                            "))
                ' docpdf.Add(New Paragraph(pstotalcompras))
            End If
            docpdf.Add(New Paragraph("                                             "))
            docpdf.Add(New Paragraph(psfechainicio))
            docpdf.Add(New Paragraph(psnombreProveedor))
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("_____________________________________________________________________________________________________________", FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                            "))
            GenerarPDF(docpdf, dgvtotales)
            docpdf.Add(New Paragraph("                                            "))
            elementosdeltabla(docpdf)
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph(pstotalpagar))
            docpdf.Add(New Paragraph("                                            "))
            elementosdelparrafofinal(docpdf)
            docpdf.Add(New Paragraph("                                            "))

            docpdf.Close()
            Process.Start(nombrearchivo)


        Catch ex As Exception

            MessageBox.Show("Se produjo el siguiente error:" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try



    End Sub

    Private Sub GenerarPDF(ByVal documento As Document, ByVal dgv As DataGridView)

        'se crea un objeto PdfTable con el numero de columnas del DataGridView

        Dim datatable As PdfPTable = New PdfPTable(dgv.ColumnCount)

        ''asignamos algunas propiedades para el diseño del pdf
        datatable.DefaultCell.Padding = 4

        'creo una variable tipo array donde contendra los tamaños de las columnas para asignar
        Dim headerwidths As Single() = TamañoColumnas(dgv)


        datatable.SetWidths(headerwidths)
        'para ver el tamaño de la tabla en la hoja
        datatable.WidthPercentage = 90
        datatable.DefaultCell.BorderWidth = 2

        'genero el encabezado del detalle del pdf

        For i As Integer = 0 To dgv.ColumnCount - 1
            '
            Dim celda As PdfPCell
            celda = New PdfPCell(New Phrase(dgv.Columns(i).HeaderText, New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 10, iTextSharp.text.Font.BOLD))))
            'celda.BorderWidth=1
            'para que salgan todos los bordes de la tabla no se les asigna ningun valor..
            celda.HorizontalAlignment = Element.ALIGN_CENTER
            celda.BackgroundColor = iTextSharp.text.Color.GRAY  '-->PODEMOS ASIGNARLE ALGUN COLOR A LOS ENCABEZADOS DE LAS COLUMNAS
            datatable.AddCell(celda)
        Next

        datatable.HeaderRows = 1

        datatable.DefaultCell.BorderWidth = 2


        'genero el cuerpo del pdf

        For i As Integer = 0 To dgv.RowCount - 1
            For j As Integer = 0 To dgv.ColumnCount - 1
                If IsDBNull(dgv(j, i).Value) = True Then
                    Dim celda As PdfPCell
                    celda = New PdfPCell(New Phrase("      ", New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7))))
                    'para que la tabla tenga todos los bordes no hay que editarle los bordes
                    celda.HorizontalAlignment = Element.ALIGN_CENTER
                    celda.VerticalAlignment = Element.ALIGN_TOP
                    datatable.AddCell(celda)

                Else
                    Dim celda As PdfPCell
                    celda = New PdfPCell(New Phrase(CStr(dgv(j, i).Value), New iTextSharp.text.Font(FontFactory.GetFont("Times New Roman", 7))))
                    If IsNumeric(dgv(j, i).Value) = True Then
                        'para los campos especiales que contienen numeros se les da otra orientacion
                        If dgv.Columns(j).Name = "Upc" Or dgv.Columns(j).Name = "Salidas" Or dgv.Columns(j).Name = "Entradas" Or dgv.Columns(j).Name = "ExistenciasAnterior" Or dgv.Columns(j).Name = "InventarioFinal" Then
                            'para que la tabla tenga todos los bordes no hay que editarle los bordes
                            celda.HorizontalAlignment = Element.ALIGN_RIGHT
                            celda.VerticalAlignment = Element.ALIGN_TOP
                            'celda.BackgroundColor = iTextSharp.text.Color.WHITE
                        Else

                        End If
                    Else

                    End If
                    datatable.AddCell(celda)


                End If



            Next

            datatable.CompleteRow()

        Next

        'SE AGREGAR LA PDFPTABLE AL DOCUMENTO

        documento.Add(datatable)



    End Sub


    Private Function TamañoColumnas(ByVal dg As DataGridView) As Single()

        Dim valores(dg.ColumnCount - 1) As Single

        For i As Integer = 0 To dg.ColumnCount - 1
            valores(i) = dg.Columns(i).Width
        Next

        Return valores

    End Function

    
    Public Class PDFPageEvent
        Implements iTextSharp.text.pdf.IPdfPageEvent

        Private msnombretienda As String

        Private msMachineInfo As String
        Private msShiftInfo As String


        Private moTemplate As PdfTemplate
        Private moCB As PdfContentByte
        Private moBF As BaseFont = Nothing

        Public Sub New( _
                    ByVal snombretienda As String, _
                     ByVal sMachineInfo As String)

            MyBase.New()
            msMachineInfo = sMachineInfo
            'msShiftInfo = sShiftInfo
            msnombretienda = snombretienda

        End Sub


        Public Sub OnCloseDocument( _
                        ByVal writer As iTextSharp.text.pdf.PdfWriter, _
                        ByVal document As iTextSharp.text.Document) _
                            Implements iTextSharp.text.pdf.IPdfPageEvent.OnCloseDocument

            moTemplate.BeginText()
            moTemplate.SetFontAndSize(moBF, 9)
            moTemplate.ShowText((writer.PageNumber - 1).ToString)
            moTemplate.EndText()
        End Sub

        Public Sub OnEndPage( _
                        ByVal writer As iTextSharp.text.pdf.PdfWriter, _
                        ByVal document As iTextSharp.text.Document) _
                            Implements iTextSharp.text.pdf.IPdfPageEvent.OnEndPage

            setHeader(writer, document)
            setFooter(writer, document)
        End Sub

        Public Sub OnChapter( _
                        ByVal writer As iTextSharp.text.pdf.PdfWriter, _
                        ByVal document As iTextSharp.text.Document, _
                        ByVal paragraphPosition As Single, ByVal title As Paragraph) _
                        Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapter
        End Sub

        Public Sub OnChapterEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) _
        Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapterEnd

        End Sub

        Public Sub OnSection(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal depth As Integer, _
       ByVal title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSection
        End Sub

        Public Sub OnParagraph(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) _
        Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraph
        End Sub

        Public Sub OnSectionEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) _
        Implements iTextSharp.text.pdf.IPdfPageEvent.OnSectionEnd
        End Sub

        Public Sub OnParagraphEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) _
        Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraphEnd
        End Sub

        Public Sub OnStartPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, _
                               ByVal document As iTextSharp.text.Document) _
                               Implements iTextSharp.text.pdf.IPdfPageEvent.OnStartPage

        End Sub

        Public Sub OnGenericTag(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal rect As iTextSharp.text.Rectangle, _
                                ByVal text As String) Implements iTextSharp.text.pdf.IPdfPageEvent.OnGenericTag
        End Sub

        Public Sub OnOpenDocument( _
                        ByVal writer As iTextSharp.text.pdf.PdfWriter, _
                        ByVal document As iTextSharp.text.Document) _
                            Implements iTextSharp.text.pdf.IPdfPageEvent.OnOpenDocument
            Try
                moBF = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                moCB = writer.DirectContent
                moTemplate = moCB.CreateTemplate(50, 50)

            Catch de As DocumentException

            Catch ioe As IOException

            End Try
        End Sub


#Region "OnPageEnd"
        Private Sub setHeader(ByVal oWriter As iTextSharp.text.pdf.PdfWriter, ByVal oDocument As Document)

            Dim oTable As New PdfPTable(2)
            Dim oCell As PdfPCell
            Dim hw As Single() = {255, 350}
            oTable.SetWidths(hw)
            oTable.DefaultCell.Padding = 0


            With oTable
                oCell = New PdfPCell(iTextSharp.text.Image.GetInstance((My.Application.Info.DirectoryPath & "\logotipo.png")))

                oCell.HorizontalAlignment = Element.ALIGN_LEFT
                oCell.BorderWidth = 0
                .AddCell(oCell)


                oCell = New PdfPCell( _
                              New iTextSharp.text.Phrase( _
                                      msMachineInfo, _
                                      FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD)))

                oCell.HorizontalAlignment = Element.ALIGN_RIGHT
                oCell.BorderWidth = 0
                .AddCell(oCell)
            End With
            'para verificar la posicion de la tabla 
            oTable.TotalWidth = oDocument.PageSize.Width - 65
            oTable.WriteSelectedRows(0, -1, 30, oDocument.PageSize.Height - 10, oWriter.DirectContent) '-20




        End Sub

        Private Sub setFooter( _
                        ByVal oWriter As iTextSharp.text.pdf.PdfWriter, _
                        ByVal oDocument As iTextSharp.text.Document)

            'three columns at bottom of page
            Dim sText As String
            Dim fLen As Single


            '---Column 2: Date/Time
            sText = Now.ToString
            fLen = moBF.GetWidthPoint(sText, 9)

            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 9)
            moCB.SetTextMatrix(30, 20)
            moCB.ShowText(sText)
            moCB.EndText()

            '---Column 1: Disclaimer
            sText = ""
            fLen = moBF.GetWidthPoint(sText, 9)

            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 9)
            moCB.SetTextMatrix(oDocument.PageSize.Width / 2 - fLen / 2, 20)
            moCB.ShowText(sText)
            moCB.EndText()



            '---Column 3: Page Number
            Dim iPageNumber As Integer = oWriter.PageNumber
            sText = "Pág. " & iPageNumber & " de "
            fLen = moBF.GetWidthPoint(sText, 9)

            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 9)
            moCB.SetTextMatrix(oDocument.PageSize.Width - 90, 20)
            moCB.ShowText(sText)
            moCB.EndText()

            moCB.AddTemplate(moTemplate, oDocument.PageSize.Width - 90 + fLen, 20)
            moCB.BeginText()
            moCB.SetFontAndSize(moBF, 9)
            moCB.SetTextMatrix(280, 820)
            moCB.EndText()


        End Sub
#End Region

    End Class


    Private Sub elementosdelparrafofinal(ByVal doc As Document)


        doc.Add(New Paragraph("         FIRMA REPRESENTANTE EMPRESA(PROVEEDOR)                                                                                                                                  FIRMA EJECUTIVO", FontFactory.GetFont("Times New Roman", 7, iTextSharp.text.Font.BOLD)))
        doc.Add(New Paragraph("             _____________________________                                            ____________________________"))


    End Sub

    Private Sub elementosdeltabla(ByVal doc As Document)


        'doc.Add(New Paragraph(" ", FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        'doc.Add(New Paragraph(totalcompra, FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        'doc.Add(New Paragraph(totalventa1, FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        'doc.Add(New Paragraph(totalespacio, FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        'doc.Add(New Paragraph(totalmonto, FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        'doc.Add(New Paragraph("_________________________________________________", FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        'doc.Add(New Paragraph(total, FontFactory.GetFont("Times New Roman", 9, iTextSharp.text.Font.BOLD)))
        doc.Add(New Paragraph(totalconvertido, FontFactory.GetFont("Times New Roman", 12, iTextSharp.text.Font.BOLD)))


    End Sub

    Public Overloads Sub crearcsv(ByVal titulo As String, ByVal nit As String, ByVal fechainicial1 As String, ByVal proveedores As String, ByVal dgvespacios As DataGridView,
                                      ByVal dgvproductosC As DataGridView, ByVal dgvproductosV As DataGridView, ByVal dgvproductosM As DataGridView, ByVal nombrevendedor As String, ByVal direccion As String,
                                      ByVal tasacambio As String, ByVal totalEspacioD As String, ByVal totalespacioQ As String, ByVal totalcompras As String, ByVal totalventa As String, ByVal totalMontod As String,
                                      ByVal totalmontoQ As String, ByVal totalQ As String, ByVal totalD As String, ByVal totalCD As String, ByVal totalcontrato As String, ByVal contrato As String, ByVal dgvtotales As DataGridView)

        Dim app As Microsoft.Office.Interop.Excel._Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Microsoft.Office.Interop.Excel._Workbook = app.Workbooks.Add(Type.Missing)
        Dim worksheet As Microsoft.Office.Interop.Excel._Worksheet = Nothing


        Dim registros As Integer
        Dim registrosm As Integer
        Dim registrosEspacios As Integer


        registrosm = dgvproductosV.Rows.Count
        registros = dgvproductosC.Rows.Count
        registrosEspacios = dgvespacios.Rows.Count


        worksheet = workbook.Sheets("Hoja1")
        worksheet = workbook.ActiveSheet


        worksheet.Cells(1, 7) = contrato
        worksheet.Rows.Item(1).Font.Bold = 1
        worksheet.Rows.Item(1).Font.size = 12
        worksheet.Rows.Item(1).HorizontalAlignment = 1

        worksheet.Cells(2, 1) = "INFORMACIÓN DEL PROVEEDOR"
        worksheet.Rows.Item(2).Font.Bold = 1
        worksheet.Rows.Item(2).Font.size = 12
        worksheet.Rows.Item(2).HorizontalAlignment = 1

        worksheet.Cells(3, 1) = "Proveedor: " & proveedores
        worksheet.Rows.Item(3).Font.Bold = 1
        worksheet.Rows.Item(3).Font.size = 10
        worksheet.Rows.Item(3).HorizontalAlignment = 1

        worksheet.Cells(4, 1) = "Direccion: " & direccion
        worksheet.Rows.Item(4).Font.Bold = 1
        worksheet.Rows.Item(4).Font.size = 10
        worksheet.Rows.Item(4).HorizontalAlignment = 1

        worksheet.Cells(5, 1) = "NIT del Proveedor :" & vbTab & nit
        worksheet.Rows.Item(5).Font.Bold = 1
        worksheet.Rows.Item(5).Font.size = 10
        worksheet.Rows.Item(5).HorizontalAlignment = 1

        worksheet.Cells(6, 1) = "Tasa de cambio a utilizar: " & vbTab & tasacambio & ""
        worksheet.Rows.Item(6).Font.Bold = 1
        worksheet.Rows.Item(6).Font.size = 10
        worksheet.Rows.Item(6).HorizontalAlignment = 1





        If registros > 0 Then

            worksheet.Cells(8, 1) = " RENTA DE ESPACIO COMERCIAL SOBRE COMPRAS DE PRODUCTO"
            worksheet.Rows.Item(8).Font.Bold = 1
            worksheet.Rows.Item(8).Font.size = 12
            worksheet.Rows.Item(8).HorizontalAlignment = 1


            If dgvproductosM.RowCount > 0 Then
                For i As Integer = 1 To dgvproductosM.Columns.Count
                    worksheet.Cells(10, i + 2) = dgvproductosM.Columns(i - 1).HeaderText
                Next

                For i As Integer = 0 To dgvproductosM.Rows.Count - 1
                    For j As Integer = 0 To dgvproductosM.Columns.Count - 1
                        worksheet.Cells(i + registrosm + 11, j + 3) = dgvproductosM.Rows(i).Cells(j).Value.ToString()
                    Next
                Next

            End If

            For k As Integer = 1 To dgvproductosC.Columns.Count
                worksheet.Cells(30, k) = dgvproductosC.Columns(k - 1).HeaderText
            Next
            Dim contador As Integer
            contador = dgvproductosM.Columns.Count + 3
            For k As Integer = 0 To dgvproductosC.Rows.Count - 1
                For l As Integer = 0 To dgvproductosC.Columns.Count - 1
                    worksheet.Cells(k + 31, l + 1) = dgvproductosC.Rows(k).Cells(l).Value.ToString()
                Next
            Next

            If dgvtotales.RowCount > 0 Then
                For k As Integer = 1 To dgvtotales.Columns.Count
                    worksheet.Cells(registros + 44, k) = dgvtotales.Columns(k - 1).HeaderText
                Next

                For k As Integer = 0 To dgvtotales.Rows.Count - 1
                    For l As Integer = 0 To dgvtotales.Columns.Count - 1
                        worksheet.Cells(k + registros + 45, l + 1) = dgvtotales.Rows(k).Cells(l).Value.ToString()
                    Next
                Next
            End If


            worksheet.Cells(registros + 40, 1) = "Mes de Cobro:" & vbTab & fechainicial1 & ""
            worksheet.Rows.Item(registros + 40).Font.Bold = 1
            worksheet.Rows.Item(registros + 40).Font.size = 12
            worksheet.Rows.Item(registros + 40).HorizontalAlignment = 1


            worksheet.Cells(registros + 41, 1) = "Nombre del Proveedor: " & vbTab & nombrevendedor & ""
            worksheet.Rows.Item(registros + 41).Font.Bold = 1
            worksheet.Rows.Item(registros + 41).Font.size = 12
            worksheet.Rows.Item(registros + 41).HorizontalAlignment = 1



            If totalD > 0 Then
                worksheet.Cells(registros + 53, 1) = " Tasa de cambio: " & vbTab & tasacambio & "   *  $. " & vbTab & totalD & "   =     Q." & vbTab & totalCD & ""
                worksheet.Rows.Item(registros + 53).Font.Bold = 1
                worksheet.Rows.Item(registros + 53).Font.size = 12
                worksheet.Rows.Item(registros + 53).HorizontalAlignment = 1
            End If


            worksheet.Cells(registros + 54, 1) = "  Total a pagar en el contrato=  Q. " & vbTab & totalcontrato & ""
            worksheet.Rows.Item(registros + 54).Font.Bold = 1
            worksheet.Rows.Item(registros + 54).Font.size = 12
            worksheet.Rows.Item(registros + 54).HorizontalAlignment = 1

            worksheet.Cells(registros + 58, 1) = "         FIRMA REPRESENTANTE EMPRESA(PROVEEDOR)                                                FIRMA EJECUTIVO"
            worksheet.Rows.Item(registros + 58).Font.Bold = 1
            worksheet.Rows.Item(registros + 58).Font.size = 12
            worksheet.Rows.Item(registros + 58).HorizontalAlignment = 1

            worksheet.Cells(registros + 60, 1) = "             _____________________________                                                  ____________________________"
            worksheet.Rows.Item(registros + 60).Font.Bold = 1
            worksheet.Rows.Item(registros + 60).Font.size = 12
            worksheet.Rows.Item(registros + 60).HorizontalAlignment = 1
            worksheet.Range("B6:" & "B" & registros).ColumnWidth = 15
            worksheet.Range("C6:C1048576").ColumnWidth = 15
            worksheet.Range("E6:E1048576").ColumnWidth = 15
            worksheet.Range("F6:F1048576").ColumnWidth = 65
            worksheet.Range("G6:G1048576").ColumnWidth = 15
            worksheet.Range("H6:H1048576").NumberFormat = "#############"
            worksheet.Range("E6:E1048576").NumberFormat = "0.00"
            worksheet.Range("F6:F1048576").NumberFormat = "0.00"
            'worksheet.Range("A16:" & "XFD" & 15 + registros).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
            'worksheet.Range("A15:H15").Font.Bold = True
            'worksheet.Range("A15:H15").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter


            'worksheet.Range("A1:" & "XFD" & 15 + registros).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
            worksheet.Range("A10:H10").Font.Bold = True
            worksheet.Range("A10:H10").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter


        End If

        If registrosEspacios > 0 Then
            worksheet.Cells(8, 1) = "ALQUILER DE ESPACIOS"
            worksheet.Rows.Item(8).Font.Bold = 1
            worksheet.Rows.Item(8).Font.size = 12
            worksheet.Rows.Item(8).HorizontalAlignment = 1

            For i As Integer = 1 To dgvespacios.Columns.Count
                worksheet.Cells(10, i + 1) = dgvespacios.Columns(i - 1).HeaderText
            Next

            For i As Integer = 0 To dgvespacios.Rows.Count - 1
                For j As Integer = 0 To dgvespacios.Columns.Count - 1
                    worksheet.Cells(i + registrosEspacios + 11, j + 2) = dgvespacios.Rows(i).Cells(j).Value.ToString()
                Next
            Next


            For k As Integer = 1 To dgvtotales.Columns.Count
                worksheet.Cells(registrosEspacios + 25, k) = dgvtotales.Columns(k - 1).HeaderText
            Next

            For k As Integer = 0 To dgvtotales.Rows.Count - 1
                For l As Integer = 0 To dgvtotales.Columns.Count - 1
                    worksheet.Cells(k + registrosEspacios + 26, l + 2) = dgvtotales.Rows(k).Cells(l).Value.ToString()
                Next
            Next


            worksheet.Cells(registros + 15, 1) = "Mes de Cobro:" & vbTab & fechainicial1 & ""
            worksheet.Rows.Item(registrosEspacios + 15).Font.Bold = 1
            worksheet.Rows.Item(registrosEspacios + 15).Font.size = 12
            worksheet.Rows.Item(registrosEspacios + 15).HorizontalAlignment = 1


            worksheet.Cells(registrosEspacios + 16, 1) = "Nombre del Proveedor: " & vbTab & nombrevendedor & ""
            worksheet.Rows.Item(registrosEspacios + 16).Font.Bold = 1
            worksheet.Rows.Item(registrosEspacios + 16).Font.size = 12
            worksheet.Rows.Item(registrosEspacios + 21).HorizontalAlignment = 1



            If totalD > 0 Then
                worksheet.Cells(registrosEspacios + 33, 1) = " Tasa de cambio: " & vbTab & tasacambio & "   *  $. " & vbTab & totalD & "   =     Q." & vbTab & totalCD & ""
                worksheet.Rows.Item(registrosEspacios + 33).Font.Bold = 1
                worksheet.Rows.Item(registrosEspacios + 33).Font.size = 12
                worksheet.Rows.Item(registrosEspacios + 33).HorizontalAlignment = 1
            End If


            worksheet.Cells(registrosEspacios + 34, 2) = "  Total a pagar en el contrato=  Q. " & vbTab & totalcontrato & ""
            worksheet.Rows.Item(registrosEspacios + 34).Font.Bold = 1
            worksheet.Rows.Item(registrosEspacios + 34).Font.size = 12
            worksheet.Rows.Item(registrosEspacios + 34).HorizontalAlignment = 1

            worksheet.Cells(registrosEspacios + 38, 2) = "         FIRMA REPRESENTANTE EMPRESA(PROVEEDOR)                                                FIRMA EJECUTIVO"
            worksheet.Rows.Item(registrosEspacios + 38).Font.Bold = 1
            worksheet.Rows.Item(registrosEspacios + 38).Font.size = 12
            worksheet.Rows.Item(registrosEspacios + 38).HorizontalAlignment = 1


            worksheet.Cells(registrosEspacios + 40, 2) = "             _____________________________                                                  ____________________________"
            worksheet.Rows.Item(registrosEspacios + 40).Font.Bold = 1
            worksheet.Rows.Item(registrosEspacios + 40).Font.size = 12
            worksheet.Rows.Item(registrosEspacios + 40).HorizontalAlignment = 1

            worksheet.Range("B6:" & "B" & registrosEspacios).ColumnWidth = 10
            worksheet.Range("B6:" & "B" & registrosEspacios).WrapText = True
            worksheet.Range("C6:C1048576").ColumnWidth = 10
            worksheet.Range("C6:C1048576").WrapText = True
            worksheet.Range("D6:D1048576").ColumnWidth = 10
            worksheet.Range("D6:D1048576").WrapText = True
            worksheet.Range("E6:E1048576").ColumnWidth = 10
            worksheet.Range("E6:E1048576").WrapText = True
            worksheet.Range("F6:F1048576").ColumnWidth = 10
            worksheet.Range("F6:F1048576").WrapText = True
            worksheet.Range("G6:G1048576").ColumnWidth = 10
            worksheet.Range("G6:G1048576").WrapText = True
            worksheet.Range("H6:H1048576").ColumnWidth = 10
            worksheet.Range("H6:H1048576").WrapText = True
            worksheet.Range("A6:A1048576").NumberFormat = "#############"
            worksheet.Range("F6:F1048576").NumberFormat = "0.00"
            worksheet.Range("H6:H1048576").NumberFormat = "0.00"
            worksheet.Range("B10:" & "XFD" & 15 + registrosEspacios).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
            worksheet.Range("B10:" & "XFD" & 15 + registrosEspacios).WrapText = True
            worksheet.Range("B10:H10").Font.Bold = True

        End If
        app.Visible = True
        app = Nothing
        workbook = Nothing
        worksheet = Nothing
        FileClose(1)
        GC.Collect()

    End Sub

End Class
