
/*
//初始化子表的复制粘贴数据的功能
var paste_input_cell = new Array("1", "2", "8", "4", "3");
InitInputCellPasteEvent();
*/

//增加单元格的复制粘贴数据的功能
function _AddPasteEvent() {
    var curtbl = _sysModel[0];
    AddSubTablePasteEvent(curtbl.subtbls[0].maxorder - 1);
}

//初始化子表的复制粘贴数据的功能
function InitInputCellPasteEvent() {
    if (document.getElementById("txtComments") == undefined) {
        document.write("<TEXTAREA STYLE=\"position:absolute;top:-100px;\" ID=\"txtComments\"></TEXTAREA>");
    }
    var curtbl = _sysModel[0];
    for (var i = 0; i < curtbl.subtbls[0].maxorder; i++) {
        AddSubTablePasteEvent(i);
    }
}

//增加单元格的复制粘贴数据的功能
function AddSubTablePasteEvent(rowIndex) {
    try {
        for (var cellIndex = 0; cellIndex < paste_input_cell.length; cellIndex++) {
            var cell_obj = document.getElementById("SubTbl00_" + rowIndex + "_" + paste_input_cell[cellIndex]);
            if (cell_obj != undefined) {
                cell_obj.attachEvent("onkeydown", PasteCellKeyDown);
            }
            /*
            var obj_name = "SubTbl00_" + rowIndex + "_" + paste_input_cell[cellIndex];
            jQuery(obj_name).live('keydown', PasteCellKeyDown);
            */
        }
    }
    catch (e) { }
    finally { }
}



var paste_eventElementID = "";
function PasteCellKeyDown() {
    //Ctrl + V
    if (window.event.ctrlKey && window.event.keyCode == 86) {
        var ss = document.getElementById("txtComments");
        ss.focus();
        ss.select();
        // 等50毫秒，keyPress事件发生了再去处理数据 
        setTimeout("dealwithPasteData()", 50);
        try {
            paste_eventElementID = window.event.srcElement.id;
        }
        catch (e) { }
        finally { }
    }
}


function dealwithPasteData(event) {
    try {
        var ss = document.getElementById("txtComments");
        ss.blur();
        var vData = ss.value;
        var aData = vData.split("\r\n");

        var arr_id = paste_eventElementID.split('_');
        var curRowIndex = parseInt(arr_id[1]);
        var InputIndex = arr_id[2];

        var curtbl = _sysModel[0];
        var newh = aData.length - curtbl.subtbls[0].maxorder + curRowIndex;
        if (newh > 0) {
            for (var i = 0; i < newh; i++) {
                _fnSubAdd(curtbl.subtbls[0].tablename);
                AddSubTablePasteEvent(curtbl.subtbls[0].maxorder - 1);
            }
        }
        for (rowIndex = curRowIndex; rowIndex < (aData.length + curRowIndex); rowIndex++) {
            var obj = document.getElementById("SubTbl00_" + rowIndex + "_" + InputIndex);
            if (obj != undefined) {
                var str_value = aData[rowIndex - curRowIndex];
                str_value = str_value.replace(/ /g, "");
                var arr_row_value = str_value.split('\t');
                var IsNextInput = false;
                var valueIndex = 0;
                for (cellIndex = 0; cellIndex < paste_input_cell.length; cellIndex++) {
                    if (paste_input_cell[cellIndex] == InputIndex) IsNextInput = true;
                    if (IsNextInput) {
                        var cell_obj = document.getElementById("SubTbl00_" + rowIndex + "_" + paste_input_cell[cellIndex]);
                        cell_obj.value = arr_row_value[valueIndex]
                        cell_obj.fireEvent("onchange");
                        valueIndex++;
                        if (valueIndex == arr_row_value.length) break;
                    }
                }
            }
        }
    }
    catch (e) { }
    finally { }
}