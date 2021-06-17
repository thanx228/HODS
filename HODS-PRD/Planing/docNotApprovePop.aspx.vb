Public Class docNotApprovePop
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lbCode.Text = Request.QueryString("rcvLocation")
            lbName.Text = Request.QueryString("locationName")
            viewData()
            lbCnt.Text = gvShow.Rows.Count
        End If
    End Sub
    Protected Sub viewData()
        Dim rType As String = Request.QueryString("rType")
        Dim location As String = Request.QueryString("rcvLocation")
        Dim endDate As String = Request.QueryString("endDate")
        Dim SQL As String = ""
        'If rType = "D3" Then
        '    SQL = " select  Q.MQ002 as typeName,(TB001+'-'+TB002) as DocNo,(SUBSTRING(TB003,7,2)+'-'+SUBSTRING(TB003,5,2)+'-'+SUBSTRING(TB003,1,4)) as DocDate,TB008 as CodeWH,TB009 as NameWH " & _
        '          " from SFCTB B left join CMSMQ Q on Q.MQ001=B.TB001 " & _
        '          " where B.TB013 = 'N' and MQ003 = '" & rType & "' and TB005='" & location & "' " & _
        '          " and TB003<='" & endDate & "'" & _
        '          " order by TB003 desc "
        'Else
        'Select
        'Switch(rType)

        Select Case rType
            Case "D4"
                SQL = "select  TC001 as 'Doc Type',TC002 as 'Doc No',(SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4)) as 'Doc Date',TC009 as 'Status'  " & _
                      " from MOCTC " & _
                      " left join CMSMD on MD001=TC005 " & _
                      " where TC001 in (select MQ001 from CMSMQ where MQ003='54') and TC009 in ('N','U') " & _
                      " and TC003<='" & endDate & "' and TC005='" & location & "'" & _
                      " order by TC001,TC002 "
            Case "D5"
                SQL = " select TA001 as 'Doc Type',TA002 as 'Doc No',(SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Doc Date',TA006 as 'Status' " & _
                      " from INVTA " & _
                      " left join CMSME on ME001=TA004 " & _
                      " where TA001='1105' AND TA006 in ('N','U') and TA003<='" & endDate & "' and TA004='" & location & "' " & _
                      " order by TA001,TA002 "
            Case "D11"
                SQL = " select TG001 as 'Doc Type',TG002 as 'Doc No',(SUBSTRING(TG042,7,2)+'-'+SUBSTRING(TG042,5,2)+'-'+SUBSTRING(TG042,1,4)) as 'Ship Date',TG023 as 'Status' " & _
                      " from COPTG " & _
                      " where  TG023 in ('N','U') and TG042<='" & endDate & "' and TG004='" & location & "' " & _
                      " order by TG001,TG002 "
            Case "D12"
                SQL = " select TG001 as 'Doc Type',TG002 as 'Doc No',(SUBSTRING(TG014,7,2)+'-'+SUBSTRING(TG014,5,2)+'-'+SUBSTRING(TG014,1,4)) as 'Doc Date',TG013 as status " & _
                      " from PURTG " & _
                      " where  TG013 in ('N','U') and TG014<='" & endDate & "' and TG005='" & location & "' " & _
                      " order by TG001,TG002 "
            Case "D13"
                SQL = " select TF001 as 'Doc Type',TF002 as 'Doc No',(SUBSTRING(TF024,7,2)+'-'+SUBSTRING(TF024,5,2)+'-'+SUBSTRING(TF024,1,4)) as 'Doc Date',TF020 as 'Status' " & _
                      " from INVTF " & _
                      " where  TF020 in ('N','U') and TF024<='" & endDate & "' and TF005='" & location & "' " & _
                      " order by TF001,TF002 "
            Case "D14"
                SQL = " select TH001 as 'Doc Type',TH002 as 'Doc No',(SUBSTRING(TH023,7,2)+'-'+SUBSTRING(TH023,5,2)+'-'+SUBSTRING(TH023,1,4)) as 'Doc Date',TH020 as 'Status' " & _
                      " from INVTH " & _
                      " where  TH020 in ('N','U') and TH023<='" & endDate & "' and TH005='" & location & "' " & _
                      " order by TH001,TH002 "
            Case "D15"
                SQL = " select TI001 as 'Doc Type',TI002 as 'Doc No',(SUBSTRING(TI004,7,2)+'-'+SUBSTRING(TI004,5,2)+'-'+SUBSTRING(TI004,1,4)) as 'Doc Date',TI010 as 'Status' " & _
                      " from ASTTI " & _
                      " where  TI010 in ('N','U') and TI004<='" & endDate & "' and TI006='" & location & "' " & _
                      " order by TI001,TI002 "
            Case Else
                SQL = " select  Q.MQ002 as 'Type Name',(TB001+'-'+TB002) as 'Doc No', " & _
                      " (SUBSTRING(TB003,7,2)+'-'+SUBSTRING(TB003,5,2)+'-'+SUBSTRING(TB003,1,4)) as 'Doc Date'," & _
                      " TB005 as 'Code From',TB006 as 'Name From',B.TB013 as 'Status'  " & _
                      " from SFCTB B left join CMSMQ Q on Q.MQ001=B.TB001 " & _
                      " where B.TB013 in ('N','U') and MQ003 = '" & rType & "'  and TB008='" & location & "' " & _
                      " and TB003<='" & endDate & "'" & _
                      " order by TB003 desc "
        End Select

        'If rType = "D4" Then 'Materials Issue
        '    SQL = "select  TC001 as 'Doc Type',TC002 as 'Doc No',(SUBSTRING(TC003,7,2)+'-'+SUBSTRING(TC003,5,2)+'-'+SUBSTRING(TC003,1,4)) as 'Doc Date'  " & _
        '          " from MOCTC " & _
        '          " left join CMSMD on MD001=TC005 " & _
        '          " where TC001 in (select MQ001 from CMSMQ where MQ003='54') and TC009='N' " & _
        '          " and TC003<='" & endDate & "' and TC005='" & location & "'" & _
        '          " order by TC001,TC002 "
        'ElseIf rType = "D5" Then 'inventory transaction order
        '    SQL = " select TA001 as 'Doc Type',TA002 as 'Doc No',(SUBSTRING(TA003,7,2)+'-'+SUBSTRING(TA003,5,2)+'-'+SUBSTRING(TA003,1,4)) as 'Doc Date' " & _
        '          " from INVTA " & _
        '          " left join CMSME on ME001=TA004 " & _
        '          " where TA001='1105' AND TA006 = 'N' and TA003<='" & endDate & "' and TA004='" & location & "' " & _
        '          " order by TA001,TA002 "
        'Else
        '    'SQL = " select TB008 as LOCATION,TB009 as 'LOCATION NAME' ,COUNT(TB008) as 'Not Aprove' " & _
        '    '      " from SFCTB where TB013 = 'N' and TB001 like '" & rType & "%' and TB003<='" & endDate & "'" & _
        '    '      " group by TB008,TB009 order by TB008 "
        '    SQL = " select  Q.MQ002 as 'Type Name',(TB001+'-'+TB002) as 'Doc No', " & _
        '          " (SUBSTRING(TB003,7,2)+'-'+SUBSTRING(TB003,5,2)+'-'+SUBSTRING(TB003,1,4)) as 'Doc Date'," & _
        '          " TB005 as 'Code From',TB006 as 'Name From'  " & _
        '          " from SFCTB B left join CMSMQ Q on Q.MQ001=B.TB001 " & _
        '          " where B.TB013 = 'N' and MQ003 = '" & rType & "'  and TB008='" & location & "' " & _
        '          " and TB003<='" & endDate & "'" & _
        '          " order by TB003 desc "

        'End If
        ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.ERP_ConnectionString)
    End Sub
End Class