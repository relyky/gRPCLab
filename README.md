# gRPCLab
gRPC 基本練習

0. 基本通訊: SayHello
1. 主機端串流 RPC (server-side streaming RPC)   
適合從 server 端取得較大資料量時使用
2. 用戶端串流 RPC (client-side streaming RPC)   
適合傳送較大資料量至 server 端時使用
3. 雙向串流 RPC (bidirectional streaming RPC)   
適合在 client 與 server 端間雙向傳送大資料量或是即時資料時使用

# 參考文件
* [C# 搭配 gRPC 中使用 stream RPC](https://blog.yowko.com/csharp-grpc-stream/)
* [1.gRPC Server & Unary Calls | gRPC in .NET 5](https://www.youtube.com/watch?v=hp5FTB7PI9s)
* [2.Server Streaming | gRPC in .NET 5](https://www.youtube.com/watch?v=F2T6xNRoa1E)
* [3.Client Streaming | gRPC in .NET 5](https://www.youtube.com/watch?v=DNxdvRQ4qRQ)
* [4.Bidirectional Streaming | gRPC in .NET 5](https://www.youtube.com/watch?v=wY4nMSUF9e0&t=1s)

## 留參未讀
* [教學課程：在 ASP.NET Core 中建立 gRPC 用戶端和伺服器](https://docs.microsoft.com/zh-tw/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-6.0&tabs=visual-studio)
* [Bi-directional streaming and introduction to gRPC on ASP.NET Core 3.0 (Part 2)](https://eddyf1xxxer.medium.com/bi-directional-streaming-and-introduction-to-grpc-on-asp-net-core-3-0-part-2-d9127a58dcdb) 
* [gRPC Client/Server Bi-Directional Streaming with C# | Visual Studio 2019](https://www.youtube.com/watch?v=6fiSsxEY4dg&ab_channel=Hacked)
* [[NET5]How to use gRPC-Web with Blazor WebAssembly on App Service](https://azure.github.io/AppService/2021/03/15/How-to-use-gRPC-Web-with-Blazor-WebAssembly-on-App-Service.html)
* [[NET6]在瀏覽器應用程式中使用 gRPC](https://docs.microsoft.com/zh-tw/aspnet/core/grpc/browser?view=aspnetcore-6.0)
* [gRPC: Authentication](https://grpc.io/docs/guides/auth/)
