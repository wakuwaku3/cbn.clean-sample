# cbn.clean-sample

Clean Architecture を参考に作った .Net Core によるアプリケーション。

## Required

- .Net core 2.1
- node.js
- [Google Cloud Pub/Sub](./.docs/pub-sub.md)

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
- [Google Cloud Pub/Sub](./.docs/pub-sub.md)
