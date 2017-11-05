/// <reference path="Validation.js" />
function IsTaiKhoanEmpty() {
    if (document.getElementById('TaiKhoan').value == "") {
        return 'Tên tài khoản là bắt buộc';
    }
    else { return ""; }
}
//function IsFirstNameInValid() {
//    if (document.getElementById('TxtFName').value.indexOf("@") != -1) {
//        return 'First Name should not contain @';
//    }
//    else { return ""; }
//}
//function IsLastNameInValid() {
//    if (document.getElementById('TxtLName').value.length >= 5) {
//        return 'Last Name should not contain more than 5 character';
//    }
//    else { return ""; }
//}
function IsMatKhauEmpty() {
    if (document.getElementById('MatKhau').value == "") {
        return 'Mật khẩu là bắt buộc';
    }
    else { return ""; }
}
//function IsSalaryInValid() {
//    if (isNaN(document.getElementById('TxtSalary').value)) {
//        return 'Enter valid salary';
//    }
//    else { return ""; }
//}
function IsValid() {

    var TaiKhoanEmptyMessage = IsTaiKhoanEmpty();
    var MatKhauInValidMessage = IsMatKhauEmpty();
   
    var FinalErrorMessage = "Errors:";
    if (TaiKhoanEmptyMessage != "")
        FinalErrorMessage += "\n" + TaiKhoanEmptyMessage;
    if (MatKhauInValidMessage != "")
        FinalErrorMessage += "\n" + MatKhauInValidMessage;
    

    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}