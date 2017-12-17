<!DOCTYPE html>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ page import="com.p3.qlba.entities.*"%>
<%@ page import="java.util.*"%>
<html lang="en">
<head>
	<title>Tư vấn </title>
	<link rel="stylesheet" type="text/css" href="/style.css">
	<script src="/webjars/jquery/jquery.min.js"></script>
    <script src="/webjars/sockjs-client/sockjs.min.js"></script>
    <script src="/webjars/stomp-websocket/stomp.min.js"></script>
	<title>Tư vấn</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="${pageContext.request.contextPath}/resources/css/header.css">
    <link rel="stylesheet" href="${pageContext.request.contextPath}/resources/css/footer.css">
    <link rel="stylesheet" href="${pageContext.request.contextPath}/resources/css/modal.css">
    <link rel="stylesheet" href="${pageContext.request.contextPath}/resources/css/doctor.css">
    <link rel="stylesheet" href="${pageContext.request.contextPath}/resources/css/chat-advice.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>
    
</head>
<!-- <script type="text/javascript" src="code.js"> -->
<script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>
<%-- <script type="text/javascript" src="<c:url value='code.js'/>"></script> --%>
<body>
	<header class="navbar navbar-default navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
            </div>
            <div class="navbar-collapse collapse navbar-right">
                <ul class="nav navbar-nav">
                    <li ><a href="<%=request.getContextPath()%>/bacsi/home_bac_si">Trang chủ</a></li>
                    <li ><a href="<%=request.getContextPath()%>/bacsi/trang_chu?action=BA">Bệnh án</a></li>
                    <li ><a href="<%=request.getContextPath()%>/bacsi/trang_chu?action=QLCC">Quản lý chứng chỉ</a></li>
                    <li class="active"><a href="<%=request.getContextPath()%>/tuvan">Tư vấn sức khỏe</a></li>
                    <li class="username dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class="hidden-xs">${bs.getTaiKhoan()}</span><b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="<%=request.getContextPath()%>/home/dangxuat">Đăng xuất</a></li>
                            <li><a href="<%=request.getContextPath()%>/bacsi/doi_mat_khau">Đổi mật khẩu</a></li>
                        </ul>
                    </li>
                </ul>
            </div><!--/.nav-collapse -->
        </div>
    </header>
    <div class="container mtb">
        <div class="row">
            <div class="col-xs-8 col-md-8">
                <div class="box box-primary">
                    <div class="box-header ui-sortable-handle" style="cursor: move;">
                        <i class="fa fa-comments-o"></i>

                        <h3 class="box-title">Chat</h3>
                        <input id="bacsi" type="hidden" value=<%=session.getAttribute("bacsi") %> disabled="disabled">
                        <input id="benhnhan" type="hidden" value=<%=session.getAttribute("benhnhan") %> disabled="disabled">
                        <input id="nguoigui" type="hidden" value=<%=session.getAttribute("nguoigui") %> disabled="disabled">
							
                    </div>
                    <div class="slimScrollDiv" style="position: relative; overflow: hidden; width: auto; height: 250px;">
                        <div class="box-body chat" id="conversation" style="overflow: scroll; width: auto; height: 250px;">
                        	
	                        		<%ArrayList<TuVan> tv = (ArrayList<TuVan>) request.getAttribute("cuocTuVan");
	        			          BenhNhan bnn=(BenhNhan) request.getAttribute("benhnhan");
	        			          if (tv != null) {
	 								for (int i=tv.size()-1; i>=0;i--) {
	 									if(tv.get(i).getNguoiGui()==0){
	 										%> 
	                    		<div class="container box-message">
				                    <span class="chat-user label label-danger right">Bệnh nhân <%=bnn.getHoTen() %></span><br><br>
				                    <p class="right"><%=tv.get(i).getTraLoi() %></p>
				                    <span class="time-left"><%=tv.get(i).getThoiGian()%></span>
				                    
				                </div>
				                	<%}else{ %>	
				                <div class="container box-message">
				                    <span class="chat-user label label-info">Tôi</span><br><br>
				                    <p><%=tv.get(i).getTraLoi() %></p>
				                    <span class="time-right"><%=tv.get(i).getThoiGian()%></span>
				                    
				                </div>	
				                <%}}} %>
						   		<div id="messages"></div>
                        </div>
                    </div>
              
						<div class="box-footer">
							<div class="cols-sm-10 ">
	                            <div class="input-group" >
	                                <input class="form-control text" style="height: 60px;" id="speech_message_text" placeholder="Type message..." type="text" required />
	                                <span class="input-group-addon" id="message_text" class="speech" onclick="record(this.id)"><i class="fa fa-microphone" aria-hidden="true"></i></span>
	                                <div class="input-group-btn">
										<button type="submit" id="send" class="btn" style="height:60px;background:#00BCD4;color:white;"><i class="fa fa-plus"></i></button>
										</div>
	                            	</div>
	                            	
	           
	                       </div>
<!-- 	                        <div class="input-group"> -->
<!-- 	                            <input class="form-control" id="text" placeholder="Type message..." required> -->
	
<!-- 	                            <div class="input-group-btn"> -->
<!-- 	                            <button type="submit" id="send" class="btn" style="height:60px;background:#00BCD4;color:white;"><i class="fa fa-plus" ></i></button> -->
<!-- 	                            </div> -->
<!-- 	                        </div> -->
	                    </div>

                </div>
            </div>
            <div class="col-xs-4 col-md-4">
                <div class="box box-primary">
                    <div class="box-header with-border">
                    <h3 class="box-title">Danh sách bệnh nhân cần tư vấn</h3>

                    </div>
                    
                    <div class="box-body">
                        <ul class="doctors-list doctor-list-in-box">
                          <%
 								ArrayList<BenhNhan> ds = (ArrayList<BenhNhan>) request.getAttribute("dsBenhNhan");
								
                             if (ds != null) {
 								for (BenhNhan benhNhan : ds) {
							%> 
								<li class="item">
		                             <div class="doctor-info">
		                                <span class="doctor-title"><%=benhNhan.getMaBenhNhan() %> - <%=benhNhan.getHoTen() %></span>
		                                    <a id= "disconnect" class="btn " href="<%=request.getContextPath()%>/tuvan?target=<%=benhNhan.getMaBenhNhan()%>"><span class="label label-danger pull-right">Disconnect</span></a>
		                                    <br>
		                                <span class="doctor-description hidden-xs">
		                                    <%=benhNhan.getEmail()%>
		                                    
		                                    </span>
		                            </div>
		                       	</li>
                             <%}} %>  
                            
                        </ul>
                    </div>
                   
                </div>
            </div>
        </div>
        
    </div>
    <div id="footerwrap">
	 	<div class="container">
		 	<div class="row">
		 		<div class="col-lg-4">
		 			<h4>Về chúng tôi</h4>
		 			<div class="hline-w"></div>
		 			<p>Trang web quản lý bệnh án điện tử - Đề tài Project 3 đồ án Hệ thống thông tin</p>
		 		</div>
		 		<div class="col-lg-4">
		 			<h4>Liên lạc</h4>
		 			<div class="hline-w"></div>
		 			<p>
		 				<a href="#"><i class="fa fa-dribbble"></i></a>
		 				<a href="#"><i class="fa fa-facebook"></i></a>
		 				<a href="#"><i class="fa fa-twitter"></i></a>
		 				<a href="#"><i class="fa fa-instagram"></i></a>
		 				<a href="#"><i class="fa fa-tumblr"></i></a>
		 			</p>
		 		</div>
		 		<div class="col-lg-4">
		 			<h4>Địa chỉ</h4>
		 			<div class="hline-w"></div>
		 			<p>
		 				Số 1<br/>
		 				Đại Cồ Việt, Hà Nội<br/>
		 				Đại học Bách Khoa Hà Nội<br/>
		 			</p>
		 		</div>
		 	
		 	</div>
	 	</div>
	 </div>

<script  type="text/javascript" src="${pageContext.request.contextPath}/resources/js/chat_bs.js"></script>
<script  type="text/javascript" src="${pageContext.request.contextPath}/resources/js/api-speech.js"></script>
</body>
</html>
