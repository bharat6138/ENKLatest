<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ENK.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        ul.list-googleAnalytics {
            margin: 0px !important;
        }

        .jarviswidget .widget-body {
            padding-bottom: 0px !important;
        }

        .switch-field {
               font-family: "Lucida Grande", Tahoma, Verdana, sans-serif;
    padding: 7px 40px;
    text-align: center;
    /* color: white; */
    overflow: hidden;
    background: #9fa775bd;
        }

        .switch-title {
            margin-bottom: 6px;
        }

        .switch-field input {
            position: absolute !important; 
            clip: rect(0, 0, 0, 0);
            height: 1px;
            width: 1px;
            border: 0;
            overflow: hidden;
        }

        .switch-field label {
            float: left;
        }

        .switch-field label {
            display: inline-block;
            width: 81px;
            height: 80px;
            line-height: 63px;
            background-color: #e4e4e4;
            color: rgba(0, 0, 0, 0.6);
            font-size: 14px;
            font-weight: normal;
            text-align: center;
            text-shadow: none;
            padding: 6px 14px;
            border: 1px solid rgba(0, 0, 0, 0.2);
            -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.3), 0 1px rgba(255, 255, 255, 0.1);
            box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.3), 0 1px rgba(255, 255, 255, 0.1);
            -webkit-transition: all 0.1s ease-in-out;
            -moz-transition: all 0.1s ease-in-out;
            -ms-transition: all 0.1s ease-in-out;
            -o-transition: all 0.1s ease-in-out;
            transition: all 0.1s ease-in-out;
        }

            .switch-field label:hover {
                cursor: pointer;
            }

        .switch-field input:checked + label {
            background-color: #A5DC86;
            -webkit-box-shadow: none;
            box-shadow: none;
        }

        .switch-field label:first-of-type {
            border-radius: 4px 0 0 4px;
        }

        .switch-field label:last-of-type {
            border-radius: 0 4px 4px 0;
        }

        .carousel-control {
            position: absolute;
            top: 0;
            left: 0;
            bottom: 0;
            width: 15%;
            opacity: .2 !important;
        }

        .carousel-indicators {
            display: none !important;
        }

        ul.list-supplierProfile > li .box div + div {
            margin-left: 0px !important;
            padding-right: 0px !important;
        }

        .table-responsive {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" role="main" style="margin-top: 0px!important;">

        <!-- MAIN CONTENT -->
        <div id="content">

            <div class="row">
                <%--<div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
						<h1 class="page-title txt-color-blueDark"><i class="fa-fw fa fa-home"></i> Dashboard <span>> My Dashboard</span></h1>
					</div>--%>
                <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
                    <asp:Label ID="lblPendingInventory" Font-Size="14px" runat="server" ForeColor="Red" Text="You have some pending inventory to accept from"></asp:Label>
                    <asp:Label ID="lblDistributorforAcceptInventory" Font-Size="14px" runat="server" ForeColor="blue" Text=""></asp:Label>
                    <asp:Button ID="lbAcceptInventory" class="btn btn-primary" runat="server" Text="Accept Inventory" OnClick="lbAcceptInventory_Click" />
                    <ul id="sparks" class="">
                        <%--<li class="sparks-info sparks-infoIndex">
								<h5> Revenue <span class="txt-color-blue">$47,171</span></h5>
								<div class="sparkline txt-color-blue hidden-mobile hidden-md hidden-sm">
									1300, 1877, 2500, 2577, 2000, 2100, 3000, 2700, 3631, 2471, 2700, 3631, 2471								</div>
							</li>--%>
                        <%--<li class="sparks-info sparks-infoIndex">
								<h5> Lane Uptime <span class="txt-color-purple">                                
                                <i class="fa fa-arrow-circle-up">
                                </i>&nbsp;45%</span></h5>
								<div class="sparkline txt-color-purple hidden-mobile hidden-md hidden-sm">
									110,150,300,130,400,240,220,310,220,300, 270, 210								</div>
							</li>--%>
                        <li class="sparks-info sparks-infoIndex">

                            <%-- <div class="sparkline txt-color-purple hidden-mobile hidden-md hidden-sm">
									<asp:Label ID="lblBalance1" runat="server" Text=""></asp:Label></div>--%>
								
								 
                        </li>
                    </ul>
                </div>
            </div>
            <!-- widget grid -->
            <section id="widget-grid" class="">
                <!-- row -->
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                        <li data-target="#myCarousel" data-slide-to="3"></li>
                    </ol>

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        <div class="item active" id="DivImg1" runat="server">
                            <asp:Image ID="Img1" runat="server" class="" alt="" Style="margin-bottom: 0px!important; width: 100%; height: 260px;" />
                        </div>

                        <div class="item" id="DivImg2" runat="server">
                            <asp:Image ID="Img2" runat="server" class="" alt="" Style="margin-bottom: 0px!important; width: 100%; height: 260px;" />
                        </div>

                        <div class="item" id="DivImg3" runat="server">
                            <asp:Image ID="Img3" runat="server" class="" alt="" Style="margin-bottom: 0px!important; width: 100%; height: 260px;" />
                        </div>
                        <div class="item" id="DivImg4" runat="server">
                            <asp:Image ID="Img4" runat="server" class="" alt="" Style="margin-bottom: 0px!important; width: 100%; height: 260px;" />
                        </div>
                    </div>

                    <!-- Left and right controls -->
                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>

                <div class="row">
                    <article class="col-sm-12">
                        <!-- new widget -->
                        <div class="jarviswidget" id="wid-id-0" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-colorbutton="false" data-widget-deletebutton="false">
                            <!-- widget options:
								usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">

								data-widget-colorbutton="false"
								data-widget-editbutton="false"
								data-widget-togglebutton="false"
								data-widget-deletebutton="false"
								data-widget-fullscreenbutton="false"
								data-widget-custombutton="false"
								data-widget-collapsed="true"
								data-widget-sortable="false"

								-->
                            <header>
                                <span class="widget-icon"><i class="glyphicon glyphicon-stats txt-color-darken"></i></span>
                                <h2>Live Feeds </h2>

                                <%--<ul class="nav nav-tabs pull-right in" id="myTab">
										<li class="active">
											<a data-toggle="tab" href="#s1">
                                                <i class="fa fa-clock-o"></i> 
                                                <span class="hidden-mobile hidden-tablet">Live Stats</span>
											</a>										

										</li>

										<li>
											<a data-toggle="tab" href="#s2"><i class="fa fa-facebook"></i> <span class="hidden-mobile hidden-tablet">Social Network</span></a>										

										</li>

										<li>
											<a data-toggle="tab" href="#s3"><i class="fa fa-dollar"></i> <span class="hidden-mobile hidden-tablet">Revenue</span></a>										

										</li> 
									</ul>--%>
                            </header>

                            <!-- widget div-->
                            <div class="no-padding">
                                <!-- widget edit box -->
                                <div class="jarviswidget-editbox">
                                </div>
                                <!-- end widget edit box -->

                                <div class="widget-body">
                                    <!-- content -->
                                    <div id="myTabContent" class="tab-content">
                                        <div class="tab-pane fade active in padding-10 no-padding-bottom" id="s1">
                                            <div class="row no-space">
                                                <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                                                </div>
                                            </div>
                                            <div class="content-in">
                                                <ul class="list-supplierProfile list-googleAnalytics">
                                                    <li>
                                                        <div>
                                                            <div class="switch-field">
                                                                 <div class="switch-title"><strong>Choose Operator</strong><br />  <strong style="color:red;">(H2O COMING SOON)</strong></div>
                                                              
                                                                <asp:HiddenField ID="hiddenNetwork" runat="server" />
                                                                <%--<input type="radio" runat="server" id="switch_left" name="switch_2" value="Lyca"   />--%>
                                                                <asp:RadioButton ID="switch_left" ClientIDMode="Static" runat="server" GroupName="GroupName" value="Lyca" AutoPostBack="true" Checked="true" OnCheckedChanged="switch_left_CheckedChanged" />
                                                                <label for="switch_left"><strong>Lyca</strong></label>
                                                                <%-- <input type="radio"  runat="server" id="switch_right" name="switch_2" value="H2O" checked  />--%>
                                                                <asp:RadioButton ID="switch_right" ClientIDMode="Static" GroupName="GroupName" AutoPostBack="true" runat="server" value="H2O" OnCheckedChanged="switch_right_CheckedChanged" />
                                                                <label for="switch_right"><strong>H2O</strong></label>

                                                            </div>


                                                        </div>
                                                        <div style="display: none">
                                                            <div style="width: 150px!important; height: 2px!important; padding: 20px 5px 15px!important;">
                                                                <asp:Label ID="lblDistributorName" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label></br>
                                                            <asp:Label ID="lblAddress1" runat="server" Style="font-size: 12px!important;" Text=""></asp:Label></br>
                                                            <asp:Label ID="lblAddress2" runat="server" Style="font-size: 12px!important;" Text=""></asp:Label></br>
                                                            <asp:Label ID="lblPhoneNumber" runat="server" Style="font-size: 12px!important;" Text=""></asp:Label>
                                                                <%-- <asp:Label ID="lblEmail" runat="server" style="width:150px!important; font-size:12px!important;" Text=""></asp:Label>                                                           --%>
                                                            </div>
                                                            <div>
                                                                <p>
                                                                    <span>
                                                                        <i style="position: initial!important;" class="fa fa-home"></i>
                                                                    </span>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <a style="display: none; background: url(../img/spirit-arrow1.png) right 3px no-repeat!important; background-color: #477ba8!important;" class="btn-addnew view-blue" title="Address">STORE INFO</a>
                                                    </li>


                                                    <li class="box-vilet">
                                                        <div class="box">


                                                            <div class="table-responsive" style="padding:2px 0 2px!important;">
                                                                <table class="table">
                                                                    <thead>
                                                                        <tr style="background-color: #fafafa00 !important; background-image: -webkit-linear-gradient(top,#f2f2f200 0,#fafafa00 100%) !important;">
                                                                            <th><strong style="font-size: 10px; color: white;">Activations</strong></th>
                                                                            <th><strong style="font-size: 10px; color: white;">Current</strong></th>
                                                                            <th><strong style="font-size: 10px; color: white;">Previous</strong></th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tr>
                                                                        <td style="font-size: 11px;">Blank SIM</td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right"><a style="color: black;" href="ActivationDetails.aspx">
                                                                            <asp:Label ID="lblActivationMonth" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label></a></td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right"><a style="color: black;" href="ActivationDetails.aspx?sim=L">
                                                                            <asp:Label ID="lblActivationMonthLast" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label></a></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-size: 11px;">Preloaded SIM</td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right"><a style="color: black;" href="ActivationDetails.aspx?sim=p">
                                                                            <asp:Label ID="lblactivationPreloadedMonth" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label></a></td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right"><a style="color: black;" href="ActivationDetails.aspx?sim=PL">
                                                                            <asp:Label ID="lblactivationPreloadedMonthLAST" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label></a></td>
                                                                    </tr>


                                                                         <tr>
                                                                        <td style="font-size: 11px;">Total</td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right"><a style="color: black;" href="ActivationDetails.aspx?sim=T">
                                                                            <asp:Label ID="lblActivation" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label></a></td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right"><a style="color: black;" href="ActivationDetails.aspx?sim=LT">
                                                                            <asp:Label ID="lblActivationLAST" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label></a></td>
                                                                    </tr>


<%--                                                                    <tr>
                                                                        <td style="font-size: 11px;">Total</td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right">
                                                                                <asp:Label ID="" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label>

                                                                        </td>
                                                                        <td style="font-size: 12px; font-weight: bold; color: black; text-align: right">
                                                                            <asp:Label ID="" Style="font-size: 12px!important; color: white;" runat="server" Text=""></asp:Label>
                                                                        </td>

                                                                    </tr>--%>
                                                                </table>
                                                            </div>
                                                        </div>

                                                    </li>

                                                    <li class="box-green">
                                                        <div class="box" style="height: 95px;">
                                                            <div style="width: 150px!important; height: 2px!important; padding: 20px 5px 15px!important;">

                                                                <asp:Label ID="lblBlankSIm" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="Label3" Style="font-size: 12px!important;" runat="server" Text="SIM"></asp:Label>
                                                                <asp:Label Visible="false" ID="lblMobileSim" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                                <asp:Label Visible="false" ID="Label2" Style="font-size: 12px!important;" runat="server" Text="Mobile SIM"></asp:Label></br>
                                                            </div>
                                                            <div>
                                                                <p>
                                                                    <span>
                                                                        <i style="position: initial!important; font-size: 44px;" class="fa fa-file-o"></i>
                                                                    </span>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <%-- <a style="background: url(../img/spirit-arrow1.png) right 3px no-repeat!important; background-color: #70BA01!important;" class="btn-addnew view-green" title="AVAILABLE"  >AVAILABLE SIM</a>--%>
                                                      <%--  <a class="btn-addnew view-green" title="AVAILABLE" href="SimDetails.aspx">
                                                            <asp:Label ID="Label6" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                            BLANK NON-PRELOADED SIMs</a>
                                                        <a runat="server" id="a2" class="btn-addnew view-green" title="AVAILABLE" href="SimDetails.aspx?sim=p">
                                                            <asp:Label ID="Label7" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                            PRELOADED SIMs</a>--%>

                                                        <a class="btn-addnew view-green" title="AVAILABLE" href="SimDetails.aspx">
                                                            <asp:Label ID="lblBlankNonPreloadedSim" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                            BLANK NON-PRELOADED SIMs</a>
                                                        <a runat="server" id="anchorBlankSIM" class="btn-addnew view-green" title="AVAILABLE" href="SimDetails.aspx?sim=p">
                                                            <asp:Label ID="lblBlankPreloadedSim" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                            PRELOADED SIMs</a>
                                                    </li>
                                                    <li class="box-red">
                                                        <div class="box" style="height: 118px;">
                                                            <div style="width: 150px!important; height: 2px!important; padding: 20px 5px 15px!important;">
                                                                Commission
                                                                <asp:Label ID="lblCommissionAmount" Style="font-size: 12px!important;" runat="server" Text="0"></asp:Label>
                                                                <asp:Label ID="lblIP" Style="font-size: 12px!important;" runat="server" Text="" Visible="false"></asp:Label></br>
                                                            <asp:Label ID="lblBrowser" Style="font-size: 12px!important;" runat="server" Text="" Visible="false">&nbsp; &nbsp;</asp:Label>
                                                                </br>
                                                            <asp:Label ID="lblLastLogin1" Style="font-size: 12px!important;" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblLastLogin2" Style="font-size: 12px!important;" runat="server" Text="" Visible="false"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <p>
                                                                    <span>
                                                                        <i style="position: initial!important; font-size: 47px;" class="fa fa-refresh"></i>
                                                                    </span>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <a style="background: url(../img/spirit-arrow1.png) right 3px no-repeat!important; background-color: #cd4544!important;" class="btn-addnew view-red" title="Last Login" href="CommissionReport.aspx">COMMISSION FOR MONTH
                                                            <asp:Label ID="lblMonth" Style="font-size: 12px!important;" runat="server" Text=""></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end content -->
                                </div>
                            </div>
                            <%--<div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">
                                <img src="img/10.jpg" class="" alt="" style="margin-bottom: 0px!important; width: 100%;">
                            </div>

                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                                <img src="img/1.jpg" class="" alt="" style="margin-bottom: 0px!important; width: 100%;">
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                                <img src="img/8.jpg" class="" alt="" style="margin-bottom: 0px!important; width: 100%;">
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg1-12">

                                <img src="img/2.jpg" class="" alt="" style="margin-bottom: 0px!important; width: 100%;">
                            </div>--%>



                            <!-- end widget div -->
                        </div>
                        <!-- end widget -->
                    </article>
                </div>
                <!-- end row -->
            </section>
            <!-- end widget grid -->
        </div>
        <!-- END MAIN CONTENT -->
    </div>
    <script type="text/javascript">
        function norm_radio_name() {
            $("[type=radio]").each(function (i) {
                var name = $(this).attr("name");
                var splitted = name.split("$");
                $(this).attr("name", splitted[splitted.length - 1]);
            });
        };

        $(document).ready(function () {
            norm_radio_name();
        });

        // for UpdatePannel
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            norm_radio_name();
        });
    </script>
</asp:Content>
