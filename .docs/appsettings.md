# アプリケーション設定

## 読み込み

書き設定を読み込んでパラメータを作成する。  
同じキー項目が存在する場合、上位のルールが採用される。  
json ファイルは存在しない場合無視される。

- command parameter  
  起動時のコマンドパラメータで下記のように指定できる。
  コロンで区切ると入れ子のパラメータになる
  ```bash
  dotnet xxx.dll hoge1=huga1 hoge2=huga2 hoge3:child=huga3
  ```
- 環境変数
- UserSecrets  
  dotnet コマンドでアセンブリごとに指定できる。
  詳細は[こちら](https://docs.microsoft.com/ja-jp/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows)
- `appsettings.user.json`  
  .gitignore でソース管理から除外される設定
- `appsettings.{hostingEnvironment.EnvironmentName}.json`
  Web のみ指定可能。
- `appsettings.json`

## パラメータ

| キー                                  | 説明                                                                   | Web | Cli | Messaging |
| ------------------------------------- | ---------------------------------------------------------------------- | --- | --- | --------- |
| ConnectionStrings:DefaultConnection   | 通常接続用の ConnectionStrings                                         | ○   | ○   | ○         |
| ConnectionStrings:AdminConnection     | マイグレーションで使う Database 作成可能なユーザーの ConnectionStrings | ○   | ○   | ○         |
| ConnectionStrings:MigrationConnection | マイグレーションで使う DDL 実行可能なユーザーの ConnectionStrings      | ○   | ○   | ○         |
| SqlPoolPath                           | Sql ファイルの置き場所                                                 | △   | △   | △         |
| JwtSecret                             | Jwt 認証の秘密鍵                                                       | ○   | ○   | ○         |
| JwtExpiresDate                        | Jwt 認証の有効期限                                                     | ○   | △   | △         |
| JwtAudience                           | Jwt 認証の検証用の項目                                                 | ○   | ○   | ○         |
| JwtIssuer                             | Jwt 認証の検証用の項目                                                 | ○   | ○   | ○         |
| Database                              | 通常接続で使う Database                                                | ○   | ○   | ○         |
| ProjectId                             | Google Cloud Pub/Sub の ProjectId                                      | ○   | ○   | ○         |
| TopicId                               | Google Cloud Pub/Sub の TopicId                                        | ○   | ○   | ○         |
| SubscriptionId                        | Google Cloud Pub/Sub の SubscriptionId                                 | ○   | ○   | ○         |
| IsEnableCors                          | CORS を有効にする場合 true を設定                                      | △   | -   | -         |
| UseAuthentication                     | 認証 を有効にする場合 true を設定                                      | △   | -   | -         |
| IsUseSecure                           | SSL 接続 を有効にする場合 true を設定                                  | △   | -   | -         |
| CorsOrigins                           | CORS のホワイトリストをコンマ区切りで指定                              | △   | -   | -         |
