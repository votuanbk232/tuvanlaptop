function NameEmpty() {
    if (document.getElementById('TxtName').value == "") {
        return 'Tên sự kiện là bắt buộc';
    }
    else { return ""; }
}
function IsValid() {

    var SuKienMessage = NameEmpty();
    var FinalErrorMessage = "Errors:";
    var FinalErrorMessage = "Errors:";
    if (SuKienMessage != "")
        FinalErrorMessage += "\n" + SuKienMessage;
    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}