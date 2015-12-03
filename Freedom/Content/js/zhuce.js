function checkna() {
    na = form1.yourname.value;
    if (na.length < 1 || na.length > 12) {
        divname.innerHTML = '<font class="tips_false">长度是1~12个字符</font>';

    } else {
        divname.innerHTML = '<font class="tips_true">输入正确</font>';

    }

}
//验证密码
function checkpsd1() {
    psd1 = form1.yourpass.value;
    var flagZM = false;
    var flagSZ = false;
    var flagQT = false;
    if (psd1.length < 6 || psd1.length > 12) {
        divpassword1.innerHTML = '<font class="tips_false">长度错误</font>';
    } else {
        for (i = 0; i < psd1.length; i++) {
            if ((psd1.charAt(i) >= 'A' && psd1.charAt(i) <= 'Z') || (psd1.charAt(i) >= 'a' && psd1.charAt(i) <= 'z')) {
                flagZM = true;
            }
            else if (psd1.charAt(i) >= '0' && psd1.charAt(i) <= '9') {
                flagSZ = true;
            } else {
                flagQT = true;
            }
        }
        if (!flagZM || !flagSZ || flagQT) {
            divpassword1.innerHTML = '<font class="tips_false">密码必须是字母数字的组合</font>';

        } else {

            divpassword1.innerHTML = '<font class="tips_true">输入正确</font>';

        }

    }
}
//验证确认密码
function checkpsd2() {
    if (form1.yourpass.value != form1.yourpass2.value) {
        divpassword2.innerHTML = '<font class="tips_false">您两次输入的密码不一样</font>';
    } else {
        divpassword2.innerHTML = '<font class="tips_true">输入正确</font>';
    }
}
//验证邮箱

function checkmail() {
    apos = form1.youremail.value.indexOf("@");
    dotpos = form1.youremail.value.lastIndexOf(".");
    if (apos < 1 || dotpos - apos < 2) {
        divmail.innerHTML = '<font class="tips_false">输入错误</font>';
    }
    else {
        divmail.innerHTML = '<font class="tips_true">输入正确</font>';
    }
}
//验证电话号码
function checktel() {
    var test = form1.yourtel.value;
    if (test > 999999999 && test < 99999999999) {
        divtel.innerHTML = '<font class="tips_true">输入正确</font>';
    }
    else {
        divtel.innerHTML = '<font class="tips_false">输入错误</font>';
    }
}
//产生验证码
window.onload = function createCode() {
    code = "";
    var codeLength = 4;//验证码的长度
    var checkCode = document.getElementById("code");
    var random = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');//随机数
    for (var i = 0; i < codeLength; i++) {//循环操作
        var index = Math.floor(Math.random() * 36);//取得随机数的索引（0~35）
        code += random[index];//根据索引取得随机数加到code上
    }
    checkCode.value = code;//把code值赋给验证码
}
//校验验证码
function checkcode() {
    var inputCode = document.getElementById("yourcode").value.toUpperCase(); //取得输入的验证码并转化为大写
    if (inputCode.length <= 0) { //若输入的验证码长度为0
        divcode.innerHTML = '<font class="tips_false">请输入验证码</font>';
    }
    else if (inputCode != code) { //若输入的验证码与产生的验证码不一致时
        divcode.innerHTML = '<font class="tips_true">输入错误</font>';
        createCode();//刷新验证码
        document.getElementById("yourcode").value = "";//清空文本框
    }
    else { //输入正确时
        divcode.innerHTML = '<font class="tips_true">输入正确</font>';
    }
}