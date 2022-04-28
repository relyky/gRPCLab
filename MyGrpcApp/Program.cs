using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using MyGrpcService;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("§§ gRPC Lab");

//using var channel = GrpcChannel.ForAddress("http://localhost:5027");
using var channel = GrpcChannel.ForAddress("https://localhost:7027");
var client = new Greeter.GreeterClient(channel);
Console.WriteLine("已建立 gRPC Context.");

while (true)
{
  Console.WriteLine(Environment.NewLine + "測試情境指令");
  Console.WriteLine("[A] 基本通訊");
  Console.WriteLine("[B] 主機端串流 gRPC (server-side streaming gRPC)");
  Console.WriteLine("[C] 用戶端串流 gRPC (client-side streaming gRPC)");
  Console.WriteLine("[D] 雙向串流 gRPC (bidirectional streaming gRPC)");
  Console.WriteLine("[Esc] 離開");

  var key = Console.ReadKey();

  if (key.Key == ConsoleKey.A)
  {
    Console.WriteLine(Environment.NewLine + "# 基本通訊 ------------");
    var result = client.SayHello(new HelloRequest { Name = "Foo" });
    Console.WriteLine($"result: {result.Message}");
    //===========================================
  }
  else if (key.Key == ConsoleKey.B)
  {
    Console.WriteLine(Environment.NewLine + "# 主機端串流 gRPC ------------");
    Console.WriteLine("適合從 server 端取得較大資料量時使用。");

    using var resp = client.DownloadCv(new DownloadByName { Name = "Bar" });
    while (resp.ResponseStream.MoveNext().Result)
    {
      var item = resp.ResponseStream.Current;
      Console.WriteLine($"{item.Name}, {item.Jobs[0].Title}, {item.Jobs[1].Title}");
    }
    //===========================================
  }
  else if (key.Key == ConsoleKey.C)
  {
    Console.WriteLine(Environment.NewLine + "# 用戶端串流 gRPC ------------");
    Console.WriteLine("適合傳送較大資料量至 server 端時使用。");

    // 模擬
    RepeatedField<Job> jobList = new();
    jobList.Add(new Job { Title = "註冊護士", Salary = 17859, JobDescription = "第一位" });
    jobList.Add(new Job { Title = "中學教師", Salary = 8716, JobDescription = "第二位" });
    jobList.Add(new Job { Title = "軟件應用程式工程師", Salary = 8405, JobDescription = "第三位" });
    jobList.Add(new Job { Title = "電工", Salary = 8021, JobDescription = "第四位" });
    jobList.Add(new Job { Title = "施工經理", Salary = 7145, JobDescription = "第五位" });
    jobList.Add(new Job { Title = "木工工匠", Salary = 6812, JobDescription = "第六位" });
    jobList.Add(new Job { Title = "金屬鉗工和機械技工", Salary = 6335, JobDescription = "第七位" });
    jobList.Add(new Job { Title = "水喉技工", Salary = 5861, JobDescription = "第八位" });
    jobList.Add(new Job { Title = "汽車維修技工", Salary = 5292, JobDescription = "第九位" });
    jobList.Add(new Job { Title = "大學講師", Salary = 5042, JobDescription = "第十位" });

    using var req = client.CreateCv();

    // 將資料逐一輸出至 server
    Candidate new1 = new() { Name = $"Baz#1" };
    new1.Jobs.AddRange(jobList);
    await req.RequestStream.WriteAsync(new1);

    // 將資料逐一輸出至 server
    Candidate new2 = new() { Name = $"Baz#2" };
    new2.Jobs.AddRange(jobList);
    await req.RequestStream.WriteAsync(new2);

    // 將資料逐一輸出至 server
    Candidate new3 = new() { Name = $"Baz#3" };
    new3.Jobs.AddRange(jobList);
    await req.RequestStream.WriteAsync(new3);

    // 將資料逐一輸出至 server
    Candidate new4 = new() { Name = $"Baz#4" };
    new4.Jobs.AddRange(jobList);
    await req.RequestStream.WriteAsync(new4);

    // 宣示完成上傳
    await req.RequestStream.CompleteAsync();

    // 等回傳結果
    var summary = await req.ResponseAsync;
    Console.WriteLine($"summary:{summary.IsSuccess},{summary.Count}");
    //===========================================
  }
  else if (key.Key == ConsoleKey.D)
  {
    Console.WriteLine(Environment.NewLine + "# 雙向串流 gRPC ------------");
    Console.WriteLine("適合在 client 與 server 端間雙向傳送大資料量或是即時資料時使用。");

    using var ctx = client.CreateDownloadCv();

    //※ 先啟動回應處理程序
    var responseHanldeTask = Task.Run(async () =>
    {
      // 逐一取出 response 內容
      while (await ctx.ResponseStream.MoveNext())
      {
        var respItem = ctx.ResponseStream.Current;
        Console.WriteLine($"{respItem.Name}");
      }
    });

    // 將資料逐一輸出至 server
    Candidate new1 = new() { Name = $"Foo" };
    await ctx.RequestStream.WriteAsync(new1);    

    // 將資料逐一輸出至 server
    Candidate new2 = new() { Name = $"Bar" };
    await ctx.RequestStream.WriteAsync(new2);

    // 模擬斷斷續續上傳
    await Task.Delay(1000);

    // 將資料逐一輸出至 server
    Candidate new3 = new() { Name = $"Baz" };
    await ctx.RequestStream.WriteAsync(new3);

    // 宣示完成上傳
    await ctx.RequestStream.CompleteAsync();

    //※ 記得要等回應處理程序完成才能結束。
    await responseHanldeTask;
  }
  else if (key.Key == ConsoleKey.Escape)
  {
    Console.WriteLine("Exit >>>>>>");
    break;
  }
}