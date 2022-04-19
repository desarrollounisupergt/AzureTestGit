Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO

Public Class exportacioncontratos
    Dim psproveedor As Paragraph
    Dim psplazo As Paragraph
    Dim psformapago As Paragraph
    Dim psotro As Paragraph
    Dim psfin As Paragraph
    Dim psfechainicial As Paragraph
    Dim psfechafin As Paragraph
    Dim psunisuper As Paragraph
    Dim psNproveerdor As Paragraph
    Dim psproveedorfac As Paragraph
    Dim contactos As String
    Dim nombreunisuper As String
    Dim proveedor As String
    Dim nombreproveedor As String
    Dim fechactual As String
    Dim hora As String
    Dim fechafinal1 As String
    Dim fechainicio1 As String
    Dim formapago1 As String
    Dim nombreproveedorfac As String
    'funcion que crea completamente el pdf


    Public Overloads Sub creararchivo(ByVal titulo As String, ByVal fechafinal As String, ByVal fechainicial As String, ByVal proveedores As String, ByVal nocontrato As String, _
                                   ByVal formapago As String, ByVal dgvcontactos As DataGridView, ByVal dgvproductos As DataGridView, _
                                      ByVal representanteU As String, representanteP As String, ByVal dgvdatos As DataGridView, ByVal dgvmarcas As DataGridView, ByVal proveedorfact As String)

        proveedor = " Proveedor:" & vbTab & proveedores
        nombreunisuper = "Representante de Unisuper:" & vbTab & representanteU
        nombreproveedor = "Representante del Proveedor:" & vbTab & representanteP
        formapago1 = "Forma de Pago:" & vbTab & formapago
        fechafinal1 = "Fecha Final del contrato:" & vbTab & fechafinal
        fechainicio1 = "Fecha de Inicio del contrato:" & vbTab & fechainicial
        nombreproveedorfac = " Proveedor a Facturar :" & vbTab & proveedorfact

        'If dgvcontactos.RowCount = 0 Then
        '    MessageBox.Show("No hay datos para crear reporte", "UniReportes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If
        'definimos la direccion del archivo...
        Dim sdireccion As String = Environ("UserProfile")
        Dim nombrearchivo As String = sdireccion & "\Mis documentos\" & titulo & ".pdf"
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
            psproveedor = New Paragraph(proveedor, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psproveedor.Alignment = Element.ALIGN_JUSTIFIED
            psformapago = New Paragraph(formapago1, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psformapago.Alignment = Element.ALIGN_JUSTIFIED
            psfechainicial = New Paragraph(fechainicio1, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psfechainicial.Alignment = Element.ALIGN_JUSTIFIED
            psfechafin = New Paragraph(fechafinal1, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psfechafin.Alignment = Element.ALIGN_JUSTIFIED
            psunisuper = New Paragraph(nombreunisuper, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psunisuper.Alignment = Element.ALIGN_JUSTIFIED
            psNproveerdor = New Paragraph(nombreproveedor, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psNproveerdor.Alignment = Element.ALIGN_JUSTIFIED
            psproveedorfac = New Paragraph(nombreproveedorfac, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            psproveedorfac.Alignment = Element.ALIGN_JUSTIFIED



            Dim oEvents As New PDFPageEvent("UNISUPER S.A." & vbNewLine, nocontrato)
            owriter.PageEvent = oEvents

            'se abre el archivo
            docpdf.Open()
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("_____________________________________________________________________________________________________________", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("ALQUILER DE ESPACIOS COMERCIALES – LA TORRE EXPRESS", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph(psfechainicial))
            docpdf.Add(New Paragraph(psfechafin))
            docpdf.Add(New Paragraph(psformapago))
            docpdf.Add(New Paragraph(psproveedor))
            docpdf.Add(New Paragraph(psproveedorfac))
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("Marcas :", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.BOLD)))
            docpdf.Add(New Paragraph("                                             "))
            GenerarPDF1Columna(docpdf, dgvmarcas)
            docpdf.Add(New Paragraph("                                             "))
            docpdf.Add(New Paragraph(psunisuper))
            docpdf.Add(New Paragraph(psNproveerdor))
            docpdf.Add(New Paragraph("Contactos del Proveedor:", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            docpdf.Add(New Paragraph("                                            "))
            GenerarPDF(docpdf, dgvcontactos)
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("Detalle de los Espacios Alquilados :", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            docpdf.Add(New Paragraph("                                             "))
            GenerarPDF(docpdf, dgvdatos)
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("Detalle de los Productos :", FontFactory.GetFont("Calibri ", 9, iTextSharp.text.Font.NORMAL)))
            docpdf.Add(New Paragraph("                                            "))
            GenerarPDF(docpdf, dgvproductos)
            docpdf.Add(New Paragraph("                                            "))
            docpdf.Add(New Paragraph("                                            "))
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
            celda = New PdfPCell(New Phrase(dgv.Columns(i).HeaderText, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 10, iTextSharp.text.Font.BOLD))))
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
                    celda = New PdfPCell(New Phrase("      ", New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 7))))
                    'para que la tabla tenga todos los bordes no hay que editarle los bordes
                    celda.HorizontalAlignment = Element.ALIGN_CENTER
                    celda.VerticalAlignment = Element.ALIGN_TOP
                    datatable.AddCell(celda)

                Else
                    Dim celda As PdfPCell
                    celda = New PdfPCell(New Phrase(CStr(dgv(j, i).Value), New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 7))))
                    If IsNumeric(dgv(j, i).Value) = True Then
                        'para los campos especiales que contienen numeros se les da otra orientacion
                        If dgv.Columns(j).Name = "Upc" Or dgv.Columns(j).Name = "Sku" Or dgv.Columns(j).Name = "Entradas" Or dgv.Columns(j).Name = "Costo con Iva" Then
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


    Private Sub GenerarPDF1Columna(ByVal documento As Document, ByVal dgv As DataGridView)

        'se crea un objeto PdfTable con el numero de columnas del DataGridView

        Dim datatable As PdfPTable = New PdfPTable(dgv.ColumnCount)

        ''asignamos algunas propiedades para el diseño del pdf
        datatable.DefaultCell.Padding = 1

        'creo una variable tipo array donde contendra los tamaños de las columnas para asignar
        Dim headerwidths As Single() = TamañoColumnas(dgv)


        datatable.SetWidths(headerwidths)
        'para ver el tamaño de la tabla en la hoja
        datatable.WidthPercentage = 50
        datatable.DefaultCell.BorderWidth = 2

        'genero el encabezado del detalle del pdf

        For i As Integer = 0 To dgv.ColumnCount - 1
            '
            Dim celda As PdfPCell
            celda = New PdfPCell(New Phrase(dgv.Columns(i).HeaderText, New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 10, iTextSharp.text.Font.BOLD))))
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
                    celda = New PdfPCell(New Phrase("      ", New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 7))))
                    'para que la tabla tenga todos los bordes no hay que editarle los bordes
                    celda.HorizontalAlignment = Element.ALIGN_CENTER
                    celda.VerticalAlignment = Element.ALIGN_TOP
                    datatable.AddCell(celda)

                Else
                    Dim celda As PdfPCell
                    celda = New PdfPCell(New Phrase(CStr(dgv(j, i).Value), New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 7))))
                    If IsNumeric(dgv(j, i).Value) = True Then
                        'para los campos especiales que contienen numeros se les da otra orientacion
                        If dgv.Columns(j).Name = "Upc" Or dgv.Columns(j).Name = "Sku" Or dgv.Columns(j).Name = "Entradas" Or dgv.Columns(j).Name = "Costo con Iva" Then
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

    Private Sub GenerarPDF2(ByVal documento As Document, ByVal dgv As DataGridView)

        'se crea un objeto PdfTable con el numero de columnas del DataGridView

        Dim datatable As PdfPTable = New PdfPTable(dgv.ColumnCount)

        ''asignamos algunas propiedades para el diseño del pdf
        datatable.DefaultCell.Padding = 2

        'creo una variable tipo array donde contendra los tamaños de las columnas para asignar
        Dim headerwidths As Single() = TamañoColumnas(dgv)
        datatable.SetWidths(headerwidths)
        'para ver el tamaño de la tabla en la hoja
        datatable.WidthPercentage = 100
        datatable.HeaderRows = 1
        'genero el cuerpo del pdf

        For i As Integer = 0 To dgv.RowCount - 1
            For j As Integer = 0 To dgv.ColumnCount - 1

                If IsDBNull(dgv(j, i).Value) = True Then
                    Dim celda As PdfPCell
                    celda = New PdfPCell(New Phrase("      ", New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 7, Font.BOLD))))
                    'para que la tabla tenga todos los bordes no hay que editarle los bordes
                    celda.BorderWidth = 0
                    celda.HorizontalAlignment = Element.ALIGN_RIGHT
                    datatable.AddCell(celda)

                Else
                    Dim celda As PdfPCell
                    celda = New PdfPCell(New Phrase(CStr(dgv(j, i).Value), New iTextSharp.text.Font(FontFactory.GetFont("Calibri ", 7, Font.BOLD))))
                    celda.BorderWidth = 0
                    If IsNumeric(dgv(j, i).Value) = True Then
                        'para los campos especiales que contienen numeros se les da otra orientacion
                        If dgv.Columns(j).Name = "Upc" Or dgv.Columns(j).Name = "Sku" Or dgv.Columns(j).Name = "Entradas" Or dgv.Columns(j).Name = "Costo con Iva" Then
                            'para que la tabla tenga todos los bordes no hay que editarle los bordes
                            celda.HorizontalAlignment = Element.ALIGN_LEFT
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

    



End Class
