create role dddsampleuser login password 'dddsampleuser';
grant all privileges on database ddd_sample to dddsampleuser;

create table users(
  user_id varchar(128) primary key
  ,name varchar(128)
  ,email varchar(128)
  ,encrepted_password varchar(128)
  ,culture varchar(16)
  ,state varchar(8)
);
