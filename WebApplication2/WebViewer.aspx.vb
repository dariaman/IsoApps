Imports System.Data.SqlClient
Imports SPGeneral
Imports Microsoft.Reporting.WebForms
Imports System.IO

Imports System
Imports System.Data
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Collections.Generic
Public Class WebViewer
    Inherits System.Web.UI.Page

    Public Property UserLogin() As WebService.ClsUser
        Get
            Return CType(Session("Users"), WebService.ClsUser)
        End Get
        Set(ByVal value As WebService.ClsUser)
            Session("Users") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not UserLogin Is Nothing Then
            If Not Page.IsPostBack Then
                If UserLogin.IsActive Then
                    Try
                        'Session("DashBoard") = "PREVIEW <i class='fa fa-star fa-fw'></i>"
                        Dim p As New clsReport
                        Dim pColl As New clsReport.MSReportCollection
                        'If Session("No") = "4" Or Session("No") = "5" Or Session("No") = "6" Or Session("No") = "7" _
                        '    Or Session("No") = "9" Or Session("No") = "10" Or Session("No") = "11" Or Session("No") = "12" Or Session("No") = "13" Or Session("No") = "14" _
                        '    Or Session("No") = "17" Or Session("No") = "18" Or Session("No") = "19" Or Session("No") = "20" Or Session("No") = "21" Or Session("No") = "22" Or Session("No") = "23" _
                        '    Or Session("No") = "25" Or Session("No") = "27" Or Session("No") = "28" Or Session("No") = "32" Or Session("No") = "33" Or Session("No") = "34" Or Session("No") = "35" _
                        '    Or Session("No") = "36" Or Session("No") = "37" Or Session("No") = "38" Or Session("No") = "39" Or Session("No") = "40" Or Session("No") = "41" Or Session("No") = "42" Or Session("No") = "43" Then
                        '    clsReport.Rpt1Key(p, pColl, Session("No"), Session("PIN"), Session("Param1"), Session("Param2"))
                        '    pColl.viewrpt(ReportViewer1, p.ReportPath, p.RptSql1, p.DSstr1, p.Param1, p.Param2, p.Param3, p.Param4, p.Param5)
                        '    Session("No_Reg") = ""
                        'End If
                        'If Session("No") = "2" Or Session("No") = "3" Or Session("No") = "30" Or Session("No") = "31" Then
                        '    clsReport.Rpt12Key(p, pColl, Session("No"), Session("kd_store"), Session("ACA_INVOICE_NO"), Session("Param1"), Session("Param2"))
                        '    pColl.viewrpt(ReportViewer1, p.ReportPath, p.RptSql1, p.DSstr1, p.Param1, p.Param2, p.Param3, p.Param4, p.Param5)
                        'End If
                        'If Session("No") = "8" Or Session("No") = "29" Then
                        '    clsReport.Rpt17Key(p, pColl, Session("No"), Session("Key1"), Session("Key2"), Session("Key3"), Session("Key4"), Session("Key5"), Session("Key6"), Session("Key7"), Session("Param1"), Session("Param2"), Session("Key8"), Session("Key9"))
                        '    pColl.viewrpt(ReportViewer1, p.ReportPath, p.RptSql1, p.DSstr1, p.Param1, p.Param2, p.Param3, p.Param4, p.Param5)
                        'End If
                        'If Session("No") = "15" Then
                        '    clsReport.Rpt12Key(p, pColl, Session("No"), Session("NO_POLIS"), Session("PIN"), Session("Param1"), Session("Param2"))
                        '    pColl.viewrpt(ReportViewer1, p.ReportPath, p.RptSql1, p.DSstr1, p.Param1, p.Param2, p.Param3, p.Param4, p.Param5)
                        'End If
                        'If Session("No") = "16" Then
                        '    clsReport.Rpt16Key(p, pColl, Session("No"), Session("Key1"), Session("Key2"), Session("Key3"), Session("Key4"), Session("Key5"), Session("Key6"), Session("Param1"), Session("Param2"), Session("Param3"), Session("Key8"))
                        '    pColl.viewrpt(ReportViewer1, p.ReportPath, p.RptSql1, p.DSstr1, p.Param1, p.Param2, p.Param3, p.Param4, p.Param5)
                        'End If
                        If Session("No") = "1" Or Session("No") = "2" Or Session("No") = "3" Or Session("No") = "4" Or Session("No") = "5" Or Session("No") = "6" Then
                            clsReport.Rpt1Key(p, pColl, Session("No"), Session("key1"), Session("Param1"), Session("Param2"))
                            pColl.viewrpt(ReportViewer1, p.ReportPath, p.RptSql1, p.DSstr1, p.Param1, p.Param2, p.Param3, p.Param4, p.Param5)
                        End If
                        
                        ReportViewer1.ShowExportControls = False
                        Session("No") = ""
                        clsReport.Bytes = pColl.exporttofile(ReportViewer1, "Pdf")

                        Dim clsrptcoll As New clsReport.MSReportCollection
                        Dim DSUtama As DataSet
                        With ReportViewer1.LocalReport
                            .ReportPath = p.ReportPath
                            DSUtama = clsrptcoll.RptQuery(p.RptSql1)
                            Dim reportDS As ReportDataSource = New ReportDataSource(p.DSstr1, DSUtama.Tables(0))
                            .ReportEmbeddedResource = p.ReportPath
                            ReportViewer1.ProcessingMode = ProcessingMode.Local
                            .DataSources.Clear()
                            .DataSources.Add(reportDS)
                            .DisplayName = "Report : " & p.DSstr1
                            If Len(p.Param1) = 0 Then p.Param1 = " "
                            If Len(p.Param2) = 0 Then p.Param2 = " "
                            If Len(p.Param3) = 0 Then p.Param3 = " "
                            If Len(p.Param4) = 0 Then p.Param4 = " "
                            If Len(p.Param5) = 0 Then p.Param5 = " "
                            Dim Param1 As New ReportParameter("Param1", p.Param1)
                            Dim Param2 As New ReportParameter("Param2", p.Param2)
                            Dim Param3 As New ReportParameter("Param3", p.Param3)
                            Dim Param4 As New ReportParameter("Param4", p.Param4)
                            Dim Param5 As New ReportParameter("Param5", p.Param5)
                            .SetParameters(Param1)
                            .SetParameters(Param2)
                            .SetParameters(Param3)
                            .SetParameters(Param4)
                            .SetParameters(Param5)
                            '.Refresh()
                        End With
                        'Dim RV1 As ReportViewer
                        'RV1 = RV
                        'Return RV1
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Download", ("window.open('WebViewerExport.aspx','_blank','width=800,height=700,top=1,left=1,scrollbars=yes,menubar=yes,resizable=yes');"), True)
                        'ClientScript.RegisterStartupScript(Me.GetType, "closePage", "<script language=javascript>window.close();</script>")

                    Catch ex As Exception
                        Session("No") = ""
                        ReportViewer1.Visible = False
                        'Image1.Visible = True
                        BtnExport.Visible = False
                        BtnExport1.Visible = False
                        'ltrlMsg.Text = sama.showMsg("Error", "Error terjadi. Mohon kontak IT support.", 4).Show(Me)
                        'Dim msg As String = String.Format("{0} - webviewer - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.SourceCode, ex, Environment.NewLine)
                        'WriteFile.Write(general.SetFullFilePath, msg)
                    End Try
                Else
                    'ClientScript.RegisterStartupScript(Me.GetType, "closePage", "<script language=javascript>window.close();</script>")
                End If
            End If
        Else
            'ClientScript.RegisterStartupScript(Me.GetType, "closePage", "<script language=javascript>window.close();</script>")
        End If
    End Sub

    Protected Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click, BtnExport1.Click
        Try
            Dim flname As String = UserLogin.UserId & Format(CDate(Now), "yyMMddffff")
            Dim fs As FileStream = New FileStream(config.RptPath & flname & ".pdf", FileMode.Create)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Download", ("window.open('WebViewerExport.aspx','_blank','width=800,height=700,top=1,left=1,scrollbars=yes,menubar=yes,resizable=yes');"), True)
            fs.Write(clsReport.Bytes, 0, clsReport.Bytes.Length)
            fs.Flush()
            fs.Close()
            Dim filePath As String = config.RptPath & flname & ".pdf"
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "attachment;filename=\" + flname + ".pdf")
            Response.TransmitFile(filePath)
            Response.End()
        Catch ex As Exception
            Dim msg As String = String.Format("{0} - webviewer - " & UserLogin.UserId & " - {1} - {2}{3}", Now.ToString("dd/MM/yyyy HH:mm:ss"), UserLogin.BranchCode, ex, Environment.NewLine)
            WriteFile.Write(config.SetFullFilePath, msg)
        End Try
        
        'Dim pColl As New clsReport.MSReportCollection
        'clsReport.Bytes = pColl.exporttofile(ReportViewer1, "Pdf")
        'Response.ClearContent()
        'Response.ClearHeaders()
        'Response.ContentType = "application/pdf"
        'Response.BinaryWrite(clsReport.Bytes)
        'Dim fileNameExtension As String = Session("JudulXls") & ".pdf"
        'HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & Replace(Replace(Session("ReportPath"), "ReportViewer/", ""), "rdlc", "") & fileNameExtension)

        'Response.Flush()
        'Response.Close()
        'Session("JudulXls") = ""
        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Download", ("window.open('WebViewerExport.aspx','_blank','width=800,height=700,top=1,left=1,scrollbars=yes,menubar=yes,resizable=yes');"), True)
    End Sub

    Protected Sub BtnExport0_Click(sender As Object, e As EventArgs) Handles BtnExport0.Click, BtnExport01.Click
        Dim pColl As New clsReport.MSReportCollection
        clsReport.Bytes = pColl.exporttofile(ReportViewer1, "Excel")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Download", ("window.open('WebViewerExportExcel.aspx','_blank','width=800,height=700,top=1,left=1,scrollbars=yes,menubar=yes,resizable=yes');"), True)

    End Sub


    Private m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    ' Export the given report as an EMF (Enhanced Metafile) file.
    Private Sub Export(ByVal report As LocalReport)
        Dim deviceInfo As String = "<DeviceInfo>" & _
        "<OutputFormat>EMF</OutputFormat>" & _
        "<PageWidth>8.5in</PageWidth>" & _
        "<PageHeight>11in</PageHeight>" & _
        "<MarginTop>0.25in</MarginTop>" & _
        "<MarginLeft>0.25in</MarginLeft>" & _
        "<MarginRight>0.25in</MarginRight>" & _
        "<MarginBottom>0.25in</MarginBottom>" & _
        "</DeviceInfo>"
        Dim warnings As Warning()
        m_streams = New List(Of Stream)()
        report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
        For Each stream As Stream In m_streams
            stream.Position = 0
        Next
    End Sub
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)
        Return stream
    End Function
    ' Handler for PrintPageEvents
    Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))

        ' Adjust rectangular area with printer margins.
        Dim adjustedRect As New Rectangle(ev.PageBounds.Left - CInt(ev.PageSettings.HardMarginX), _
        ev.PageBounds.Top - CInt(ev.PageSettings.HardMarginY), _
        ev.PageBounds.Width, _
        ev.PageBounds.Height)

        ' Draw a white background for the report
        ev.Graphics.FillRectangle(Brushes.White, adjustedRect)

        ' Draw the report content
        ev.Graphics.DrawImage(pageImage, adjustedRect)

        ' Prepare for the next page. Make sure we haven't hit the end.
        m_currentPageIndex += 1
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
    End Sub

    Private Sub Print()
        If m_streams Is Nothing OrElse m_streams.Count = 0 Then
            Throw New Exception("Error: no stream to print.")
        End If
        Dim printDoc As New PrintDocument()
        If Not printDoc.PrinterSettings.IsValid Then
            Throw New Exception("Error: cannot find the default printer.")
        Else
            AddHandler printDoc.PrintPage, AddressOf PrintPage
            m_currentPageIndex = 0
            printDoc.Print()
        End If
    End Sub

    ' Create a local report for Report.rdlc, load the data,
    ' export the report to an .emf file, and print it.
    'Private Sub Run()
    Private Sub PrintPO()
        Dim report As New LocalReport()
        report = ReportViewer1.LocalReport
        Export(report)
        Print()

    End Sub

    Public Overloads Sub Dispose()
        If m_streams IsNot Nothing Then
            For Each stream As Stream In m_streams
                stream.Close()
            Next
            m_streams = Nothing
        End If
    End Sub

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        PrintPO()
    End Sub
End Class