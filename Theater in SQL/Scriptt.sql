--Kreating tables

create table "User" (
"Id" SERIAL primary key,
"Name" VARCHAR(255) not null,
"Email" VARCHAR(255) not null unique
);

create table "Payment" (
"Id" SERIAL primary key,
"Date" TIMESTAMP default CURRENT_TIMESTAMP,
"Method" Varchar(255) default 'cash',
check ("Method" in ('credit card', 'cash'))
);

create table "Theater" (
"Id" SERIAL primary key,
"Name" VARCHAR(255) not null,
"NumberOfSeats" INT not null
);

create table "Actor" (
"Id" SERIAL primary key,
"FirstName" VARCHAR(255) not null,
"LastName" VARCHAR(255),
"DateOfBirth" DATE
)

create table "Show" (
"Id" SERIAL primary key,
"Name" VARCHAR(255) not null,
"Genre" VARCHAR(255),
"Duration" INT
)

create table "ShowActor" (
"Id" SERIAL primary key,
"ShowId" INT not null,
"ActorId" INT not null,
"Role" VARCHAR(255),
constraint "FK_ShowActor_Show_ShowId" foreign key ("ShowId") references "Show" ("Id"),
constraint "FK_ShowActor_Actor_ActorId" foreign key ("ActorId") references "Actor" ("Id")
)

create table "TheaterShow" (
"Id" SERIAL primary key,
"ShowId" INT not null,
"TheaterId" INT not null,
"Time" TIMESTAMP,
constraint "FK_TheaterShow_Show_ShowId" foreign key ("ShowId") references "Show" ("Id"),
constraint "FK_TheaterShow_Theater_TheaterId" foreign key ("TheaterId") references "Theater" ("Id")
)

create table "Ticket" (
"Id" SERIAL primary key,
"TheaterShowId" INT not null,
"ShowId" INT not null,
"UserId" INT not null,
"SeatNumber" INT,
"PaymentId" INT not null,
"Price" INT,
constraint "FK_Ticket_TheaterShow_TheaterShowId" foreign key ("TheaterShowId") references "TheaterShow" ("Id"),
constraint "FK_Ticket_Show_ShowId" foreign key ("ShowId") references "Show" ("Id"),
constraint "FK_Ticket_User_UserId" foreign key ("UserId") references "User" ("Id"),
constraint "FK_Ticket_Payment_PaymentId" foreign key ("PaymentId") references "Payment" ("Id")
)

--Inserting data

insert into "User" ("Name", "Email") values
('Jure','jure@gmail.com'),
('David','david1@gmail.com'),
('Tomica','tomotomic@gmail.com'),
('Antisa', 'antee@gmail.com');

insert into "Actor" ("FirstName", "LastName", "DateOfBirth") values 
('Leonardo', 'DiCaprio', '1974-11-11'),
('Meryl', 'Streep', '1949-06-22'),
('Denzel', 'Washington', '1954-12-28'),
('Scarlett', 'Johansson', '1984-11-22'),
('Morgan', 'Freeman', '1937-06-01');

insert into "Payment" default values;
insert into "Payment" ("Date", "Method") values
('2025-01-01 10:00:00', 'credit card'),
('2025-01-02 12:30:00', 'cash'),
('2025-01-03 15:00:00', 'credit card');

insert into "Theater" ("Name", "NumberOfSeats") values
('Grand Theater', 200),
('City Center Theater', 150),
('Broadway Hall', 300),
('Downtown Cinema', 100),
('Starview Theater', 250);

insert into "Show" ("Name", "Genre", "Duration") values
('Hamlet', 'Drama', 120),
('The Lion King', 'Musical', 150),
('The Play That Goes Wrong', 'Comedy', 148),
('Waiting for Godot', 'Tragicomedy', 120),
('The Nutcracker', 'Ballet', 135);

insert into "ShowActor" ("ShowId", "ActorId", "Role") values
(1, 2, 'Hamlet'),
(2, 2, 'Mufasa'),
(3, 1, 'Dom Cobb'),
(5, 4, 'Iron Man'),
(5, 3, 'Clara');

insert into "TheaterShow" ("ShowId", "TheaterId", "Time") values
(1, 1, '2025-01-01 19:00:00'),
(2, 2, '2025-01-02 20:00:00'),
(3, 3, '2025-01-03 21:00:00'),
(4, 4, '2025-01-04 18:00:00'),
(5, 5, '2025-01-05 17:00:00');

insert into "Ticket" ("TheaterShowId", "ShowId", "UserId", "SeatNumber", "PaymentId", "Price") values
(1, 1, 1, 10, 1, 100),
(2, 2, 2, 20, 2, 120),
(3, 3, 3, 30, 3, 150),
(4, 4, 4, 40, 4, 200);

--Commadns

update "User" set "Name" = 'Ivan' where "Id" = 2;
update "User" set "Email" = 'ivanivanic@gmail.com' where "Id" = 2;

--View all data

select
"User"."Name" as "User Name",
"User"."Email" as "User Email",
"Show"."Name" as "Show Name",
"Show"."Genre" as "Show Genre",
"Theater"."Name" as "Theater Name",
"Theater"."NumberOfSeats" as "Theater Capacity",
"Ticket"."SeatNumber" as "Seat Number",
"Payment"."Method" as "Payment Method",
"Payment"."Date" as "Payment Date",
"Ticket"."Price" as "Ticket Price"
from "Ticket"
join "User" on "Ticket"."UserId" = "User"."Id"
join "Show" on "Ticket"."ShowId" = "Show"."Id"
join "TheaterShow" on "Ticket"."TheaterShowId" = "TheaterShow"."Id"
join "Theater" on "TheaterShow"."TheaterId" = "Theater"."Id"
join "Payment" on "Ticket"."PaymentId" = "Payment"."Id";

select
"Show"."Name" as "Show Name",
"Show"."Genre" as "Show Genre",
"Actor"."FirstName" || ' ' || "Actor"."LastName" as "Actor",
"Show"."Duration" as "Show Duration",
"Ticket"."Price" as "Ticket Price"
from "Show"
join "ShowActor" on "Show"."Id" = "ShowActor"."ShowId"
join "Actor" on "ShowActor"."ActorId" = "Actor"."Id"
join "Ticket" on "Show"."Id" = "Ticket"."ShowId";

--Mora jedan pa drugi zbog foreign key constraint-a
--Opcija: modificirat strani ključ u tablici ShowActor da uključuje ON DELETE CASCADE.

delete from "ShowActor" where "Id" = 4;
delete from "Actor" where "Id" = 4;

--View all tables

select * from "Actor";
select * from "Show";
select * from "Payment";
select * from "Theater";
select * from "ShowActor";
select * from "TheaterShow";
select * from "Ticket";