<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecodeCorrect.aspx.cs" Inherits="ConfirmOnline.Operation.RecodeCorrect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Container" runat="server">
        <div class="row" style="height:20px;"></div>
        <div class="row center-block">
            <div class="well col-md-6 col-md-offset-3 col-centered text-center">
                <h2>请核实确认您的信息</h2>
                    <div id="divContainer" style="margin-top: 30px;" runat="server">
                    </div>
                <asp:HiddenField ID="HiddenField" runat="server" />
                <asp:LinkButton ID="btn_Submit" type="submit" class="btn btn-info btn-block" style="margin-top: 25px;" runat="server" Text="点击完成核实" OnClick="btn_Submit_Click"/>
            </div>
        </div>
    </div>

    <asp:GridView ID="DataGrid1" runat="server">
    </asp:GridView>

<!-- Modal -->
<div class="modal fade" id="correctModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">修订信息</h4>
      </div>
      <div class="modal-body">
        <p>输入您的新信息</p>
        <input id="correctTxt" class="form-control">
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
        <button type="button" id="btn-modal-ok" class="btn btn-primary">暂存</button>
      </div>
    </div>
  </div>
</div>
<script>
    var currCorrect;
    var currButton;

    $('#correctModal').on('show.bs.modal', function (event) {
        currButton = $(event.relatedTarget);
        currCorrect = $(event.relatedTarget.parentNode.parentNode).find('.correctDataForm'); // Button that triggered the modal
        $('#correctTxt').val(currCorrect.val());
        $('#correctTxt').attr("Value", currCorrect.val());
    });

    $('#btn-modal-ok').on('click', function (event) {
        if (currCorrect.val() != $('#correctTxt').val()) {
            if ($('[id$=HiddenField]').val() != "") $('[id$=HiddenField]').val($('[id$=HiddenField]').val() + ";");
            $('[id$=HiddenField]').val($('[id$=HiddenField]').val() + escape(currCorrect.attr("id")) + ":" + escape(currCorrect.val()) + "," + escape($('#correctTxt').val()));
            currCorrect.val($('#correctTxt').val());
            currButton.removeClass("glyphicon-pencil");
            currButton.text("再次改动");
            currButton.addClass("glyphicon-ok-sign active");
            $('[id$=btn_Submit]').text(" 点击提交修订");
            $('[id$=btn_Submit]').removeClass("btn-info");
            $('[id$=btn_Submit]').addClass("btn-success glyphicon glyphicon-info-sign");
        }
        $('#correctModal').modal('hide');
    });
</script>
</asp:Content>
