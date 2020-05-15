# Backends

### 博物馆应用平台后端。
使用 `ASP.NET Core Web 应用程序` > `API控制器` 创建
### 说明
1. 新闻信息相关操作
	* 查询全部新闻信息 GET 
		访问https://localhost:随机端口号/api/news
	* 添加新数据 POST 
		访问https://localhost:随机端口号/api/news 
		示例，放正文里，json格式
		{"id":2,"title":"title","content":"天气真好","museum":"海南博物馆","publishtime":"2020-05-15 16:50:00","analyseresult":"positive"}
	* 更新、删除等请访问官方文档
		https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio
2. 配置信息查询
	访问https://localhost:随机端口号/api/configuration/[apiconfig、autorun、crawlerconfig、keywords]
3. 新闻内容正负向查询 
	访问https://localhost:随机端口号/api/analyse/analyse/?id=1
