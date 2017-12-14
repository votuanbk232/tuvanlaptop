<html lang="en">
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ page import="com.p3.qlba.entities.*"%>
<%@ page import="java.util.*"%>
<head>
<meta charset="utf-8">
<title>Quản lý lĩnh vực khám chữa</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet"
	href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<link rel="stylesheet"
	href="${pageContext.request.contextPath}/resources/css/header.css">
<link rel="stylesheet"
	href="${pageContext.request.contextPath}/resources/css/modal.css">
<link rel="stylesheet"
	href="${pageContext.request.contextPath}/resources/css/admin.css">
<link rel="stylesheet" href="${pageContext.request.contextPath}/resources/css/footer.css">
<script
	src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script
	src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<link
	href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css"
	rel='stylesheet' type='text/css'>
</head>
<body>
	<header class="navbar navbar-default navbar-fixed-top"
		role="navigation">
		<div class="container">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle" data-toggle="collapse"
					data-target=".navbar-collapse">
					<span class="sr-only">Toggle navigation</span> <span
						class="icon-bar"></span> <span class="icon-bar"></span> <span
						class="icon-bar"></span>
				</button>
				<%--                 <a class="navbar-brand" href="<%=request.getContextPath() %>/admin/home_admin">EHR.</a> --%>
			</div>
			<div class="navbar-collapse collapse navbar-right">
				<ul class="nav navbar-nav">
					<%--                     <li><a href="<%=request.getContextPath()%>/admin/home_admin">Trang chủ</a></li> --%>
					<li class="active"><a
						href="<%=request.getContextPath()%>/admin/trang_chu?action=QLLVKC">Quản
							lý lĩnh vực khám chữa</a></li>
					<li class="dropdown"><a href="#" class="dropdown-toggle"
						data-toggle="dropdown">Quản lý người dùng<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a
								href="<%=request.getContextPath()%>/admin/trang_chu?action=QLBS">Bác
									sĩ</a></li>
							<li><a
								href="<%=request.getContextPath()%>/admin/trang_chu?action=QLBN">Bệnh
									nhân</a></li>
						</ul></li>
					<li class="username dropdown"><a href="#"
						class="dropdown-toggle" data-toggle="dropdown">Admin<b
							class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a href="<%=request.getContextPath()%>/home/dangxuat">Đăng
									xuất</a></li>
							<li><a href="<%=request.getContextPath()%>/admin/doi_mat_khau">Đổi mật khẩu</a></li>
						</ul></li>
				</ul>
			</div>
			<!--/.nav-collapse -->
		</div>
	</header>
	

	<div class="container mtb">
	<!-- 	Thêm chỗ này vào tất cả cái trang khác -->
		<div class="row" style="text-align:center;color:#00BCD4;">
			<h4>
			<% 
			if (session.getAttribute("them_xoa_sua")!=null){
				out.println(session.getAttribute("them_xoa_sua").equals("thanhcong")?"Tác vụ được thực hiện thành công":"Tác vụ thất bại");
			}
			session.removeAttribute("them_xoa_sua");
			%>
			</h4>
		</div>
	
    
		<c:out value="${sessionScope.them_xoa_sua}" />
		<c:remove var="them_xoa_sua" scope="session" />
<!-- 	Thêm chỗ này vào tất cả cái trang khác -->
		<div id="qlba-modal" class="modal">
			<div class="modal-content">
				<div class="modal-header">
					<span class="close" id="close-add">&times;</span>
					<h2>Thêm lĩnh vực khám bệnh</h2>
				</div>
				<div class="modal-body">
					<form action="<%=request.getContextPath()%>/admin/them_linh_vuc_kham_chua"
						method="POST" class="form" role="form">
						<div class="cols-sm-10">
                            <div class="input-group" style="width:50%">
                                <input class="form-control" id="speech_tenLinhVuc" name="tenLinhVuc" placeholder="Tên lĩnh vực khám bệnh" type="text" required/>
                                <span class="input-group-addon" id="tenLinhVuc" class="speech" onclick="record(this.id)"><i class="fa fa-microphone" aria-hidden="true"></i></span>
                            </div>
                       </div>
                        <div class="cols-sm-10">
                            <div class="input-group" >
                                <textarea class="form-control" id="speech_moTa" name="moTa" placeholder="Mô tả" required></textarea>
                                <span class="input-group-addon" id="moTa" class="speech" onclick="record(this.id)"><i class="fa fa-microphone" aria-hidden="true"></i></span>
                            </div>
                        </div>
						
						<button class="button button-block" type="submit">Thêm
							lĩnh vực mới</button>

					</form>
				</div>
				<div class="modal-footer"></div>

			</div>
		</div>


		<div class="row">
			<div class="col-lg-10 col-lg-offset-1 centered">
				<div class="panel panel-default panel-table">
					<div class="panel-heading">
						<div class="row">

							<div class="col-xs-6 col-md-6">
								<legend> Danh sách lĩnh vực khám chữa</legend>
							</div>
							<div class="col-xs-6 col-md-6">
								<button id="btn-modal-add" class="button button-block">Thêm
									lĩnh vực khám chữa</button>
							</div>

							<input type="text" id="search_text" onkeyup="search()"
								placeholder="Gõ mã lĩnh vực khám chữa">
						</div>
					</div>
					<div class="panel-body">

						<table class="table table-striped table-bordered table-list"
							id="search-table">
							<thead>
								<tr>
									<th class="hidden-xs" style="text-align: center;">Mã lĩnh
										vực</th>
									<th style="text-align: center;">Tên lĩnh vực</th>
									<th style="text-align: center;">Mô tả</th>
									<th style="text-align: center;"></th>
								</tr>
							</thead>

							<tbody>
								<%
									ArrayList<LinhVucKhamChua> ds = (ArrayList<LinhVucKhamChua>) request.getAttribute("dSLinhVucKhamChua");
									if (ds != null) {
										for (LinhVucKhamChua linhVucKhamChua : ds) {
								%>
								<tr>
									<td class="hidden-xs" style="text-align: center;"><%=linhVucKhamChua.getMaLinhVuc()%></td>
									<td style="text-align: center;"><%=linhVucKhamChua.getTenLinhVuc()%></td>
									<td style="text-align: center;"><%=linhVucKhamChua.getMoTa()%></td>

									<td style="text-align: center;"><a class="btn btn-danger"
										href="<%=request.getContextPath()%>/admin/xoa_linh_vuc_kham_chua?id=<%=linhVucKhamChua.getMaLinhVuc()%>">
											<em class="fa fa-trash"></em>
									</a></td>
									<!-- sua thong tin  -->
									<td style="text-align: center;"><a class="btn btn-primary"
										href="#qlba_modal_update_<%=linhVucKhamChua.getMaLinhVuc()%>"
										data-toggle="modal" data-backdrop="false"><em
											class="fa fa-pencil"></em></a> <!--truyen gia tri id vao day qlba_modal_update_{}-->
										<div style="text-align: left;"
											id="qlba_modal_update_<%=linhVucKhamChua.getMaLinhVuc()%>"
											class="modal fade" role="dialog"
											aria-labelledby="myModalLabel" aria-hidden="true">

											<div class="modal-content">
												<div class="modal-header">
													<span class="close" id="close-update" data-dismiss="modal">&times;</span>
													<h2>Lĩnh vực khám chữa xem chi tiết</h2>
												</div>
												
												<div class="modal-body">
													<strong><i class="fa fa-book margin-r-5"></i>Thông
														tin lĩnh vực khám chữa</strong>
													<p class="text-muted">
														Lĩnh vực:
														<%=linhVucKhamChua.getTenLinhVuc()%>
													</p>
													<p class="text-muted">
														Mô tả chi tiết:
														<%=linhVucKhamChua.getMoTa()%>
													</p>
													<div class="row">
														<div id="update_lvkb_<%=linhVucKhamChua.getMaLinhVuc() %>" class="collapse">
															<form action="<%=request.getContextPath()%>/admin/sua_linh_vuc_kham_chua?maLinhVuc=<%=linhVucKhamChua.getMaLinhVuc()%>" method="POST" class="form" role="form">
																<strong><i class="fa fa-book margin-r-5"></i>Thay đổi</strong> 
																<input class="form-control" name="tenLinhVuc"
																	value="<%=linhVucKhamChua.getTenLinhVuc()%>"
																	type="text" required style="width: 50%" />
																<textarea class="form-control" name="moTa" required><%=linhVucKhamChua.getMoTa()%></textarea>
																<button class="button button-block" type="submit">
																	Cập nhật</button>

															</form>
														</div>
													</div>
													<a data-toggle="collapse" data-target="#update_lvkb_<%=linhVucKhamChua.getMaLinhVuc() %>">Thay
														đổi thông tin lĩnh vực</a>

												</div>
												<div class="modal-footer"></div>

											</div>


										</div></td>
								</tr>
								<%
									}
									}
								%>

							</tbody>
						</table>
					</div>
					<!--                     <div class="panel-footer"> -->
					<!--                         <div class="row"> -->
					<!--                             <div class="col col-xs-4">Page 1 of 5 -->
					<!--                             </div> -->
					<!--                             <div class="col col-xs-8"> -->
					<!--                                 <ul class="pagination hidden-xs pull-right"> -->
					<!--                                     <li><a href="#">1</a></li> -->
					<!--                                     <li><a href="#">2</a></li> -->
					<!--                                     <li><a href="#">3</a></li> -->
					<!--                                     <li><a href="#">4</a></li> -->
					<!--                                     <li><a href="#">5</a></li> -->
					<!--                                 </ul> -->
					<!--                                 <ul class="pagination visible-xs pull-right"> -->
					<!--                                     <li><a href="#">«</a></li> -->
					<!--                                     <li><a href="#">»</a></li> -->
					<!--                                 </ul> -->
					<!--                             </div> -->
					<!--                         </div> -->


					<!--                     </div> -->
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
	 <script src="${pageContext.request.contextPath}/resources/js/api-speech.js">
	    
	 </script>
	<script
		src="${pageContext.request.contextPath}/resources/js/modal-add.js"></script>
	<script
		src="${pageContext.request.contextPath}/resources/js/modal-update.js"></script>
	<script src="${pageContext.request.contextPath}/resources/js/search.js"></script>


</body>
</html>
                       
                               