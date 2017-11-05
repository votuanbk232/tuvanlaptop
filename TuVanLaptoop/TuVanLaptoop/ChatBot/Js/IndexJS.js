
      
        var username="";
function send_message(message) {
    //lưu text trong container:
    var preMessage = $('#container').html();
    //kiểm tra độ dài preMessage:18
    //console.log(preMessage.length);
    //kiểm tra message rỗng
    if (preMessage.length>18) {
        preMessage += '<br/>';
    }
    //text sau khi gửi thêm message
    $('#container').html(preMessage + "<span class='current_message'>"+"<span class='bot'>ChatBot:</span>" + message+"</span>");
    //animation: ẩn rồi hiện
    $('.current_message').hide();
    $('.current_message').delay(500).fadeIn();
    $('.current_message').removeClass('current_message');
}
function get_username() {

    send_message("Hello,What is your name?")
};
function ai(message) {
    //lấy mặc định độ dài username là 5: VoTuan
    //ban đầu username là rỗng nên luôn luôn <5, gán username là message đầu tiên:VoTuan
    if (username.length < 5) {
        username = message;
        send_message("Nice to meet you " + username + ",What do you want?");
    }
    //lần thứ 2 gọi lại, thì username có độ dài là 5, nên ko xuất ra câu lệnh bên trên nữa

    //reactions
    //if (message.indexOf("How are you") >= 0) {
    //    send_message("Thanks,I am good!");
    //}
    //if (message.indexOf("time") >= 0) {
    //    var date = new Date();
    //    var h = date.getHours();
    //    var m = date.getMinutes();
    //    send_message("Current time is: " + h + ":" + m);
    //}
    if (message.indexOf("dell") >= 0) {
        send_message("<a href='http://localhost:28617/ChatBot/GetResult' target='_blank'>http://localhost:28617/ChatBot/GetResult</a><br>Do you want anything else?");
       
        }
}
$(function () {
    //gọi hàm chatbot
    get_username();
    //sự kiện checkedbox và enter
    $('#textbox').keypress(function (event) {
        //13 là enter
        if (event.which == 13) {
            //checkbox đã đc tích
            if ($('#enter').prop("checked")) {
                //console.log("enter pressed,checkbox is checked!");
                //khi nhấn enter và chọn tích checkbox, tương đương ấn enter
                $('#send').click();
                event.preventDefault();
            }
        }
    });
    //tạo hàm chatbot
           
    //sự kiện button click
    $('#send').click(function () {
                
        //thêm user name
        var username = "<span class='username'=>You:</span>";
        var newMessage = $('#textbox').val();
        //thiết lập textbox về rỗng mỗi khi gửi message
        $('#textbox').val('');
        //lưu text trong container:
        var preMessage = $('#container').html();
        //kiểm tra độ dài preMessage:18
        //console.log(preMessage.length);
        //kiểm tra message rỗng
        if (preMessage.length > 18) {
            preMessage += '<br/>';
        }
        //text sau khi gửi thêm message
        $('#container').html(preMessage+ username + newMessage);
        //nhìn thấy message mới nhất khi ấn gửi, scroll kéo theo dòng mới nhất
        $('#container').scrollTop($('#container').prop("scrollHeight"));
        //gọi tới ai
        ai(newMessage);
    });
});
