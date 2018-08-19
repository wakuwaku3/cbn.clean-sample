# cbn.ddd-sample

.Net Core によるアプリケーションに、ドメイン駆動設計(DDD)の考え方を取り入れた実装例

## 使い方

```bash
npm install
npm start
```

- ビルド
  `npm run build`
- リリースビルド
  `npm run publish`
- テスト
  `npm run test`

## 目的

- 複数のエントリポイントが存在する場合にロジックを使い回せるようにする
- ビジネスロジックをドメイン層に集約し、再利用可能にする
- 外部ライブラリやエンティティのアクセサをインフラストラクチャ層に集約し、差し替えを容易にする
- CQRS によってオーバーヘッドを減らす
- テストを容易にする

## 構造

- アプリケーション層  
  様々なアプリケーションの形式をこえてリクエストを受けつける層。  
   ConsoleApp,WebApp 等に公開する。
  - Service  
    LifeCycle の管理やドメイン層やインフラストラクチャ層と対話して処理を行うオブジェクト。  
    ここにビジネスロジックは直接定義しない。
    Service は外部から呼び出されるが、内部からの再利用は行わない。
- ドメイン層  
   ビジネスロジックを定義する層。
  - Query  
    参照等、副作用を含まないビジネスロジックを記述するオブジェクト。
  - Command  
    更新等、副作用を含むビジネスロジックを記述するオブジェクト。  
    パラメータを受け取って副作用処理を実行する。
- インフラストラクチャ層
  外部ライブラリのラッパーやエンティティのアクセサ、外部連携等の、アプリケーション内部で実装するビジネスロジック以外のロジックを記述する層。

## 規約

### 命名

- [Microsoft のドキュメント](https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)に従う。
- Service,Query,Command を実装する場合、クラス名のサフィックスに各単語を付与する。

### DI

- 可能な限りコンストラクタ解決で実装する
- IDisposable なオブジェクト等、インスタンス生成にオーバーヘッドがある場合、解決する時 Lazy で実装する
- ドメイン層、インフラストラクチャ層以下のオブジェクトはスレッドセーフに実装する
- LifetimeCycle の優先度は singleton > lifetime > instance とする
- using はドメイン層以下には記述しない
- try-catch は 基盤 で対応するためできるだけ書かない