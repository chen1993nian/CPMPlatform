function changeTree() {
    var tablename = document.getElementById("hidTableName").value;
    var AppID = document.getElementById("hidAppID").value;
    var url = "../SysFolder/AppFrame/AppDetail.aspx?tblName=" + tablename + "&condition=_autoid='" + AppID + "'";
    document.getElementById("frm1").src = url;
}

$(function () {
    $("#layout1").ligerLayout({
        rightWidth: 350,
        topHeight: 30,
        bottomHeight: 30
    });
    var h = $(window).height() - 40;
    $("#accordion1").ligerAccordion(
    {
        height: (h - 40)
    });
    document.getElementById("frm1").style.height = h + "px";
    changeTree();
});


function gdSave() {
    //保存设置
    var tablename = document.getElementById("hidTableName").value;
    var hidAppID = document.getElementById("hidAppID").value;
    var keys = [];
    keys.push("FwTitle=" + document.getElementById("txtBT").value);
    keys.push("FwCode=" + document.getElementById("txtWH").value);
    keys.push("FwTitle2=" + document.getElementById("txtFBT").value);
    keys.push("FwYear=" + document.getElementById("txtND").value);
    keys.push("DeptCode=" + document.getElementById("txtJGH").value);
    keys.push("DutyName=" + document.getElementById("txtZRR").value);
    keys.push("DeptName=" + document.getElementById("txtBMMC").value);
    keys.push("CompanyName=" + document.getElementById("txtGSMC").value);
    keys.push("Location=" + document.getElementById("txtPos").value);
    keys.push("LocationID=" + document.getElementById("hidPos").value);
    keys.push("Catalog=" + document.getElementById("txtCtl").value);
    keys.push("CatalogID=" + document.getElementById("hidCtl").value);
    keys.push("GBTName=" + document.getElementById("txtGB").value);
    keys.push("GBTCode=" + document.getElementById("hidGB").value);
    keys.push("DATName=" + document.getElementById("txtZD").value);
    keys.push("DATCode=" + document.getElementById("hidZD").value);
    keys.push("ProjectWBSName=" + document.getElementById("txtWBS").value);
    keys.push("ProjectWBSCode=" + document.getElementById("hidWBS").value);
    keys.push("SafeDataClassName=" + document.getElementById("txtAQ").value);
    keys.push("SafeDataClassCode=" + document.getElementById("hidAQ").value);
    keys.push("QDataClassName=" + document.getElementById("txtZL").value);
    keys.push("QDataClassCode=" + document.getElementById("hidZL").value);
    keys.push("JSDataClassName=" + document.getElementById("txtJS").value);
    keys.push("JSDataClassCode=" + document.getElementById("hidJS").value);
    keys.push("ProjectName=" + document.getElementById("txtProjectName").value);
    keys.push("ProjectID=" + document.getElementById("hidProjectID").value);
    keys.push("KeyWords=" + document.getElementById("txtKeyWord").value);
    keys.push("Note=" + document.getElementById("txtReMark").value);
    keys.push("InstanceID=" + document.getElementById("hidInstanceID").value);
    keys.push("WorkflowID=" + document.getElementById("hidWorkflowID").value);
    var da_Config = EIS.Web.CDEJ.Doc.DaEdit;
    var ret = da_Config.SaveArchiveInfo(tablename, hidAppID, keys.join("|")).value;
    alert("归档完毕！");
}
