## Installing ef
```sh
dotnet tool install --global dotnet-ef
```



## Generating Migrations
```sh
dotnet ef migrations add <name>
```

## Applying Migrations
```sh
dotnet ef database update
```


## More Reads
[Microsoft Docs EF Getting Started](https://docs.microsoft.com/en-us/ef/core/get-started/?wt.mc_id=personal-blog-chnoring&tabs=netcore-cli)
[SoftChris Blog](https://softchris.github.io/pages/dotnet-orm.html#create-the-database)
