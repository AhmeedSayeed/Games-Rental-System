/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     5/10/2025 4:52:53 PM                         */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GAME') and o.name = 'FK_GAME_ADDS_ADMIN')
alter table GAME
   drop constraint FK_GAME_ADDS_ADMIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GAME') and o.name = 'FK_GAME_CATEGORIZ_CATEGORY')
alter table GAME
   drop constraint FK_GAME_CATEGORIZ_CATEGORY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GAME') and o.name = 'FK_GAME_OFFERS_VENDOR')
alter table GAME
   drop constraint FK_GAME_OFFERS_VENDOR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PAYMENT') and o.name = 'FK_PAYMENT_HAS_PAYME_RENTALS')
alter table PAYMENT
   drop constraint FK_PAYMENT_HAS_PAYME_RENTALS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RENTALS') and o.name = 'FK_RENTALS_HAS_BEEN__GAME')
alter table RENTALS
   drop constraint FK_RENTALS_HAS_BEEN__GAME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RENTALS') and o.name = 'FK_RENTALS_RENTS_USER')
alter table RENTALS
   drop constraint FK_RENTALS_RENTS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REVIEWS') and o.name = 'FK_REVIEWS_GET_REVIE_GAME')
alter table REVIEWS
   drop constraint FK_REVIEWS_GET_REVIE_GAME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REVIEWS') and o.name = 'FK_REVIEWS_MAKE_REVI_USER')
alter table REVIEWS
   drop constraint FK_REVIEWS_MAKE_REVI_USER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ADMIN')
            and   type = 'U')
   drop table ADMIN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CATEGORY')
            and   type = 'U')
   drop table CATEGORY
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GAME')
            and   name  = 'CATEGORIZED_AS_FK'
            and   indid > 0
            and   indid < 255)
   drop index GAME.CATEGORIZED_AS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GAME')
            and   name  = 'OFFERS_FK'
            and   indid > 0
            and   indid < 255)
   drop index GAME.OFFERS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GAME')
            and   name  = 'ADDS_FK'
            and   indid > 0
            and   indid < 255)
   drop index GAME.ADDS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GAME')
            and   type = 'U')
   drop table GAME
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PAYMENT')
            and   name  = 'HAS_PAYMENT2_FK'
            and   indid > 0
            and   indid < 255)
   drop index PAYMENT.HAS_PAYMENT2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PAYMENT')
            and   type = 'U')
   drop table PAYMENT
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RENTALS')
            and   name  = 'HAS_BEEN_RENTED_FK'
            and   indid > 0
            and   indid < 255)
   drop index RENTALS.HAS_BEEN_RENTED_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RENTALS')
            and   name  = 'RENTS_FK'
            and   indid > 0
            and   indid < 255)
   drop index RENTALS.RENTS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RENTALS')
            and   type = 'U')
   drop table RENTALS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('REVIEWS')
            and   name  = 'GET_REVIEW_FK'
            and   indid > 0
            and   indid < 255)
   drop index REVIEWS.GET_REVIEW_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('REVIEWS')
            and   name  = 'MAKE_REVIEW_FK'
            and   indid > 0
            and   indid < 255)
   drop index REVIEWS.MAKE_REVIEW_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('REVIEWS')
            and   type = 'U')
   drop table REVIEWS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"USER"')
            and   type = 'U')
   drop table "USER"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VENDOR')
            and   type = 'U')
   drop table VENDOR
go

/*==============================================================*/
/* Table: ADMIN                                                 */
/*==============================================================*/
create table ADMIN (
   AID                  int                  not null,
   ADMIN_NAME           varchar(50)          not null,
   USER_PHONE           varchar(11)          not null,
   USER_EMAIL           varchar(50)          not null,
   constraint PK_ADMIN primary key nonclustered (AID)
)
go

/*==============================================================*/
/* Table: CATEGORY                                              */
/*==============================================================*/
create table CATEGORY (
   CID                  int                  not null,
   CATEGORY_NAME        varchar(50)          not null,
   constraint PK_CATEGORY primary key nonclustered (CID)
)
go

/*==============================================================*/
/* Table: GAME                                                  */
/*==============================================================*/
create table GAME (
   GID                  int                  not null,
   AID                  int                  not null,
   VID                  int                  not null,
   CID                  int                  not null,
   GAME_NAME            varchar(50)          not null,
   PRICE                float                not null,
   RELEASE_DATE         datetime             not null,
   DESCRIPTION          varchar(1024)        not null,
   constraint PK_GAME primary key nonclustered (GID)
)
go

/*==============================================================*/
/* Index: ADDS_FK                                               */
/*==============================================================*/
create index ADDS_FK on GAME (
AID ASC
)
go

/*==============================================================*/
/* Index: OFFERS_FK                                             */
/*==============================================================*/
create index OFFERS_FK on GAME (
VID ASC
)
go

/*==============================================================*/
/* Index: CATEGORIZED_AS_FK                                     */
/*==============================================================*/
create index CATEGORIZED_AS_FK on GAME (
CID ASC
)
go

/*==============================================================*/
/* Table: PAYMENT                                               */
/*==============================================================*/
create table PAYMENT (
   PAYMENT_NO           int                  not null,
   UID                  int                  not null,
   GID                  int                  not null,
   RENT_DATE            datetime             not null,
   PAYMENT_DATE         datetime             not null,
   AMOUNT               float                not null,
   constraint PK_PAYMENT primary key nonclustered (PAYMENT_NO, UID, GID, RENT_DATE)
)
go

/*==============================================================*/
/* Index: HAS_PAYMENT2_FK                                       */
/*==============================================================*/
create index HAS_PAYMENT2_FK on PAYMENT (
UID ASC,
GID ASC,
RENT_DATE ASC
)
go

/*==============================================================*/
/* Table: RENTALS                                               */
/*==============================================================*/
create table RENTALS (
   UID                  int                  not null,
   GID                  int                  not null,
   RENT_DATE            datetime             not null,
   RETURN_DATE          datetime             not null,
   constraint PK_RENTALS primary key nonclustered (UID, GID, RENT_DATE)
)
go

/*==============================================================*/
/* Index: RENTS_FK                                              */
/*==============================================================*/
create index RENTS_FK on RENTALS (
UID ASC
)
go

/*==============================================================*/
/* Index: HAS_BEEN_RENTED_FK                                    */
/*==============================================================*/
create index HAS_BEEN_RENTED_FK on RENTALS (
GID ASC
)
go

/*==============================================================*/
/* Table: REVIEWS                                               */
/*==============================================================*/
create table REVIEWS (
   UID                  int                  not null,
   GID                  int                  not null,
   REVIEW_CONTENT       varchar(1024)        not null,
   constraint PK_REVIEWS primary key (UID, GID)
)
go

/*==============================================================*/
/* Index: MAKE_REVIEW_FK                                        */
/*==============================================================*/
create index MAKE_REVIEW_FK on REVIEWS (
UID ASC
)
go

/*==============================================================*/
/* Index: GET_REVIEW_FK                                         */
/*==============================================================*/
create index GET_REVIEW_FK on REVIEWS (
GID ASC
)
go

/*==============================================================*/
/* Table: "USER"                                                */
/*==============================================================*/
create table "USER" (
   UID                  int                  not null,
   USER_NAME            varchar(50)          not null,
   USER_EMAIL           varchar(50)          not null,
   USER_PHONE           varchar(11)          not null,
   JOIN_DATE            datetime             not null,
   constraint PK_USER primary key nonclustered (UID)
)
go

/*==============================================================*/
/* Table: VENDOR                                                */
/*==============================================================*/
create table VENDOR (
   VID                  int                  not null,
   VENDOR_NAME          varchar(50)          not null,
   VENDOR_EMAIL         varchar(50)          not null,
   ADDRESS              varchar(50)          not null,
   VENDOR_PHONE         varchar(11)          not null,
   constraint PK_VENDOR primary key nonclustered (VID)
)
go

alter table GAME
   add constraint FK_GAME_ADDS_ADMIN foreign key (AID)
      references ADMIN (AID)
go

alter table GAME
   add constraint FK_GAME_CATEGORIZ_CATEGORY foreign key (CID)
      references CATEGORY (CID)
go

alter table GAME
   add constraint FK_GAME_OFFERS_VENDOR foreign key (VID)
      references VENDOR (VID)
go

alter table PAYMENT
   add constraint FK_PAYMENT_HAS_PAYME_RENTALS foreign key (UID, GID, RENT_DATE)
      references RENTALS (UID, GID, RENT_DATE)
go

alter table RENTALS
   add constraint FK_RENTALS_HAS_BEEN__GAME foreign key (GID)
      references GAME (GID)
go

alter table RENTALS
   add constraint FK_RENTALS_RENTS_USER foreign key (UID)
      references "USER" (UID)
go

alter table REVIEWS
   add constraint FK_REVIEWS_GET_REVIE_GAME foreign key (GID)
      references GAME (GID)
go

alter table REVIEWS
   add constraint FK_REVIEWS_MAKE_REVI_USER foreign key (UID)
      references "USER" (UID)
go


/*Inserting Data*/
-- Clean old data
DELETE FROM PAYMENT;
DELETE FROM REVIEWS;
DELETE FROM RENTALS;
DELETE FROM GAME;
DELETE FROM ADMIN;
DELETE FROM VENDOR;
DELETE FROM CATEGORY;
DELETE FROM [USER];

-- Insert into USER table
INSERT INTO [USER] VALUES 
(1, 'Ahmed Mohamed', 'ahmed@example.com', '01012345678', '2023-01-15'),
(2, 'Fatima Ali', 'fatima@example.com', '01023456789', '2023-02-20'),
(3, 'Omar Hassan', 'omar@example.com', '01034567890', '2023-03-10'),
(4, 'Layla Ibrahim', 'layla@example.com', '01045678901', '2023-04-05'),
(5, 'Karim Mahmoud', 'karim@example.com', '01056789012', '2023-05-12');

-- Insert into CATEGORY table
INSERT INTO CATEGORY VALUES
(1, 'Action'),
(2, 'Adventure'),
(3, 'Sports'),
(4, 'Strategy'),
(5, 'Puzzle');

-- Insert into VENDOR table
INSERT INTO VENDOR VALUES
(1, 'Cairo Games', 'cairo@games.com', 'Maadi, Cairo', '02025123456'),
(2, 'Alexandria Entertainment', 'alex@entertainment.com', 'Alexandria', '03425789012'),
(3, 'Delta Digital', 'delta@digital.com', 'Mansoura', '05025741236'),
(4, 'Upper Games', 'upper@games.com', 'Luxor', '09523698741');

-- Insert into ADMIN table
INSERT INTO ADMIN VALUES
(1, 'Mahmoud Admin', '01112345678', 'mahmoud@admin.com'),
(2, 'Sara Admin', '01223456789', 'sara@admin.com');

-- Insert into GAME table
INSERT INTO GAME VALUES
(1, 1, 1, 1, 'Desert Warriors', 49.99, '2023-03-15', 'Action game set in Egyptian deserts'),
(2, 1, 2, 2, 'Pharaoh Tomb', 59.99, '2023-04-20', 'Adventure in ancient Egyptian pyramids'),
(3, 2, 3, 3, 'Cairo Football 2024', 39.99, '2023-09-10', 'Football simulation with Egyptian teams'),
(4, 2, 4, 4, 'Nile Empire', 29.99, '2023-07-05', 'Strategy game set along the Nile River'),
(5, 1, 1, 5, 'Hieroglyphic Puzzles', 19.99, '2023-05-12', 'Puzzle game based on hieroglyphics'),
(6, 2, 2, 1, 'Alexandria Rally', 34.99, '2023-08-18', 'Racing through streets of Alexandria'),
(7, 1, 3, 2, 'Legends of Sinai', 54.99, '2023-06-22', 'RPG set in the Sinai Peninsula'),
(8, 2, 4, 3, 'Hotel Tycoon Egypt', 24.99, '2023-10-30', 'Simulation game managing Egyptian resorts');

-- Insert into RENTALS table - April 2025 is the "last month" in our test scenario
-- Game 1 has the most rentals
INSERT INTO RENTALS VALUES
(1, 1, '2025-04-01', '2025-04-07'),
(2, 1, '2025-04-05', '2025-04-12'),
(3, 1, '2025-04-10', '2025-04-17'),
(1, 2, '2025-04-02', '2025-04-09'),
(2, 2, '2025-04-12', '2025-04-19'),
(3, 3, '2025-04-05', '2025-04-12'),
(4, 3, '2025-04-18', '2025-04-25'),
(1, 4, '2025-04-08', '2025-04-15'),
(2, 5, '2025-04-10', '2025-04-17'),
(3, 6, '2025-04-12', '2025-04-19'),
(1, 7, '2025-04-22', '2025-04-29'),
(5, 3, '2025-03-15', '2025-03-22'); -- A rental from previous month

-- Insert into REVIEWS table
INSERT INTO REVIEWS VALUES
(1, 1, 'Great action game with stunning desert graphics!'),
(2, 1, 'Challenging gameplay but very enjoyable'),
(3, 1, 'Love the Egyptian setting and characters'),
(1, 2, 'Amazing adventure in the pyramids'),
(2, 2, 'The puzzles are quite challenging'),
(3, 3, 'Realistic football simulation with Egyptian teams'),
(4, 3, 'Fun to play but needs more teams');

-- Insert into PAYMENT table
INSERT INTO PAYMENT VALUES
(1, 1, 1, '2025-04-01', '2025-04-01', 10.00),
(2, 2, 1, '2025-04-05', '2025-04-05', 10.00),
(3, 3, 1, '2025-04-10', '2025-04-10', 10.00),
(4, 1, 2, '2025-04-02', '2025-04-02', 12.00),
(5, 2, 2, '2025-04-12', '2025-04-12', 12.00),
(6, 3, 3, '2025-04-05', '2025-04-05', 8.00),
(7, 4, 3, '2025-04-18', '2025-04-18', 8.00),
(8, 1, 4, '2025-04-08', '2025-04-08', 7.50),
(9, 2, 5, '2025-04-10', '2025-04-10', 5.00),
(10, 3, 6, '2025-04-12', '2025-04-12', 7.00),
(11, 1, 7, '2025-04-22', '2025-04-22', 11.00),
(12, 5, 3, '2025-03-15', '2025-03-15', 8.00);