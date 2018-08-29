const gulp = require('gulp');
const {
  clean,
  restore,
  build,
  test,
  pack,
  publish,
  run
} = require('gulp-dotnet-cli');
const runSequence = require('run-sequence');
const path = require('path');
const zip = require('gulp-zip');
const spawn = require('child_process').spawn;

const webName = 'Cbn.DDDSample.Web';
const cliName = 'Cbn.DDDSample.Cli';
const subscriberName = 'Cbn.DDDSample.Subscriber';
const web = '**/' + webName + '.csproj';
const cli = '**/' + cliName + '.csproj';
const subscriber = '**/' + subscriberName + '.csproj';
const appEntries = [web, cli, subscriber];
const testEntries = [];
const entries = appEntries.concat(testEntries);
const dist = 'dist';

//clean
gulp.task('clean', () => {
  return gulp.src(entries, { read: false }).pipe(clean());
});
//restore nuget packages
gulp.task('restore', () => {
  return gulp.src(entries, { read: false }).pipe(restore());
});
//compile
gulp.task('build', () => {
  return gulp.src(entries, { read: false }).pipe(build());
});
gulp.task('build-web', () => {
  return gulp.src(web, { read: false }).pipe(build());
});
gulp.task('build-cli', () => {
  return gulp.src(cli, { read: false }).pipe(build());
});
gulp.task('build-subscriber', () => {
  return gulp.src(subscriber, { read: false }).pipe(build());
});
//run unit tests
gulp.task('test', () => {
  return gulp.src(testEntries, { read: false }).pipe(test());
});
//compile and publish an application to the local filesystem
gulp.task('publish', cb => {
  return runSequence('test', ['publish-web-fdd'], 'zip', cb);
});
gulp.task('publish-web-fdd', cb => {
  const output = path.join(process.cwd(), dist, 'raw', 'web', 'fdd');
  return gulp
    .src(web, { read: false })
    .pipe(publish({ configuration: 'Release', output: output }));
});
gulp.task('zip', cb => {
  const raw = dist + '/raw';
  return gulp
    .src(raw + '/**/*', { base: raw })
    .pipe(zip('application.zip'))
    .pipe(gulp.dest(dist));
});
gulp.task('run-web', () => {
  const web = spawn('dotnet run', {
    cwd: webName,
    shell: true
  });
  web.stdout.on('data', data => console.log('stdout: ' + data));
  web.stderr.on('data', data => console.log('stdout: ' + data));
  return web;
});
gulp.task('run-subscriber', () => {
  const subscriber = spawn('dotnet run', {
    cwd: subscriberName,
    shell: true
  });
  subscriber.stdout.on('data', data => console.log('stdout: ' + data));
  subscriber.stderr.on('data', data => console.log('stdout: ' + data));
  return subscriber;
});
gulp.task('db', cb => {
  const docker = spawn('bash .docker/database/run.sh', { shell: true });
  docker.stdout.on('data', data => console.log('stdout: ' + data));
  docker.stderr.on('data', data => console.log('stdout: ' + data));
  return docker;
});
gulp.task('migration', () => {
  const migration = spawn('dotnet run migration', {
    cwd: cliName,
    shell: true
  });
  migration.stdout.on('data', data => console.log('stdout: ' + data));
  migration.stderr.on('data', data => console.log('stdout: ' + data));
  return migration;
});
