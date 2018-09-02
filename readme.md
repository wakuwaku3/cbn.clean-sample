# cbn.clean-sample

Clean Architecture を参考に作った .Net Core によるアプリケーション。

## Required

- .Net core 2.1
- node.js
- AWS SQS  
  [アプリケーション設定](./.docs/appsettings.md)に認証情報を追加する

#### Options

- vscode
- Docker で Database を作成する場合
  - docker
  - docker-compose
  - linux

## Usage

#### 始めに

`npm install`

#### ビルド

`npm run build`

#### テスト

`npm run test`

#### Database を Docker で起動

`npm run db`

##### Docker 上の postgresql にログインする

`docker exec -ti database_clean-sample-postgres_1 psql -U postgres`

#### WebApi を実行

`npm run web`

#### Messaging Subscriber を実行

`npm run subscriber`

#### Debug

vscode でデバッグ実行可能

## 関連ドキュメント

- [アーキテクチャ](./.docs/architecture-design.md)
- [規約](./.docs/rules.md)
- [データ移行](./.docs/db-migration.md)
- [アプリケーション設定](./.docs/appsettings.md)
