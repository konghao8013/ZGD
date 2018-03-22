<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_txt.aspx.cs" Inherits="ZGD.Web.Admin.wechat.fodder.dialog_txt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../../js/layout.js"></script>
<link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    //窗口API
    var api = frameElement.api, W = api.opener;
    api.button({
        name: '确定',
        focus: true,
        callback: function () {
            submitForm();
            return false;
        }
    }, {
        name: '取消'
    });
    var action = "add";
    var id = 0;

    


    //页面加载完成执行
    $(function () {
        if ($(api.data).length > 0) {
            var parentObj = $(api.data.obj).parent().parent().parent(); //取得节点父对象
            var con = $(parentObj).find(".txt").html();

            con = replaceAll("<br>", "\n", con);
            //con = replaceAll("<","&lt;",  con);
            //con = replaceAll(">", "&gt;", con);
            


            id = api.data.id;
            $("#txtCount").val(con);
            action = "edit";
        }
    });

    //提交表单处理
    function submitForm() {
        //验证表单
        if ($("#txtCount").val() == "") {
            W.$.dialog.alert('请填文本内容', function () { $("#txtCount").focus(); }, api);
            return false;
        }
        con = $("#txtCount").val();
        $.post("dialog_txt.aspx", { action: action, id: id, txt: con }, function (e) {
            if (e == "yes") {
                if ($(api.data).length > 0) {
                    var parentObj = $(api.data.obj).parent().parent().parent(); //取得节点父对象
                    con = replaceAll("\n","<br>",  con);
                    con = replaceAll("&lt;","<",  con);
                    con = replaceAll("&gt;", ">", con);

                    $(parentObj).find(".txt").html(con);
                } else {
                    api.fun();
                }
                //因为IE9下出现JS提错，所以加上setTimeout
                setTimeout(function () {
                    api.close();
                }, 0);
            } else
            {
                W.$.dialog.alert('保存异常', function () { api.close(); }, api);
            }
        }, "text");
    } 
</script>
</head>

<body>
<div class="div-content">
    <dl>
      <dt>文本内容</dt>
      <dd>
        <textarea name="txtCount"   style="width:420px;height:200px;" id="txtCount" class="input" datatype="*1-255" sucmsg=" "></textarea>
        <span class="Validform_checktip"></span>
      </dd>
    </dl>
</div>
</body>
</html>
