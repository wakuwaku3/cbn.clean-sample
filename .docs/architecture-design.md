# アーキテクチャ設計

クリーン アーキテクチャおよび CQRS に則った設計を行う。

## 目的

- 複数のエントリポイントが存在する場合にロジックを使い回せるようにする
- ビジネスロジックをドメイン層に集約し、再利用可能にする
- 外部ライブラリやエンティティのアクセサをインフラストラクチャ層に集約し、差し替えおよび他システムでの再利用を容易にする
- テストを容易にする
- ロジック再利用時のオーバーヘッドを減らす

## 構成

- Domain Model
  ビジネスロジックを書く層
  - Cbn.CleanSample.Domain.Common  
    共通で使うビジネスロジックを定義
  - Cbn.CleanSample.Domain.Account  
    アカウントに関するビジネスロジックを定義
- Use Case
  - Cbn.CleanSample.UseCases  
    業務の手順を記述する。Transaction 管理等の Lifetime 管理が必要な場合ここに記述する。
- Interface Adapter  
  外界との接続や、外界とドメインモデル間のデータ変換をする場所。エントリーポイントを含む。
  - Cbn.CleanSample.Cli  
    Command Line Interface
  - Cbn.CleanSample.Messaging.Receiver  
    Messaging queue の受け取り
  - Cbn.CleanSample.Web  
    WebApi
  - Cbn.Infrastructure.Common
    External Interface を DIP で記述するための主に interface を記述する。
- External Interface  
   実際に外界に接続するための実装。
  - Cbn.Infrastructure.CleanSampleData  
    postgres の CleanSampleData からのデータを I/O を隠蔽
  - Cbn.Infrastructure.AspNetCore  
    WebApi の framework を部分的に隠蔽
  - Cbn.Infrastructure.Autofac
    DI コンテナ を隠蔽
  - Cbn.Infrastructure.JsonWebToken
    認証サービス を隠蔽
  - Cbn.Infrastructure.Npgsql.Entity
    postgres のドライバーである Npgsql.EntityFrameworkCore と Dapper を隠蔽
  - Cbn.Infrastructure.PubSub
    Messaging Service の Google Cloud Pub/Sub を隠蔽
- Tests
  - Cbn.Infrastructure.TestTools
    Unit Test 用の Utility
  - Cbn.CleanSample.Domain.Account.Tests
    Cbn.CleanSample.Domain.Account の Unit Test
