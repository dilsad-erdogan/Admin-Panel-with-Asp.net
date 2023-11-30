// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function kodOlustur() {
    var cb1 = document.getElementById("checkbox1");
    var cb2 = document.getElementById("checkbox2");
    var name = document.getElementById("name").value;
    var inputCode = document.getElementById("code");
    var code = "";

    switch (cb1.checked) {
        case true:
            code += "1";
            break;
        case false:
            code += "0";
            break;
        default:
            break;
    }

    switch (cb2.checked) {
        case true:
            code += "1";
            break;
        case false:
            code += "0";
            break;
        default:
            break;
    }

    //if (cb1.checked) {
    //    code = "1";
    //} else {
    //    code = "0";
    //}

    //if (cb2.checked) {
    //    code += "1";
    //} else {
    //    code += "0";
    //}

    code += name.substring(0, 2);
    inputCode.value = code;
}

//function picChange() {
//    document.getElementById("picture").ariaReadOnly = true;
//}