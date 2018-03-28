# 基于microsoft/dotnet:latest构建Docker Image
FROM microsoft/dotnet:latest
 
# 进入docker中的/usr/local/src目录
RUN cd /usr/local/src
 
# 创建DockerWebAPI目录
RUN mkdir newspaperapi
 
# 设置工作路径
WORKDIR /usr/local/src/newspaperapi
 
# 将当前文件夹下的所有文件全部复制到工作目录
COPY *.* ./
 
# 向外界暴露5000端口
EXPOSE 2300
 
# 执行dotnet DockerWebAPI.dll命令
CMD ["dotnet", "WebApi.dll"]