/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     5/13/2025                                    */
/* Modified to use auto-generated IDs                           */
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
   AID                  int                  IDENTITY(1,1) not null,
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
   CID                  int                  IDENTITY(1,1) not null,
   CATEGORY_NAME        varchar(50)          not null,
   constraint PK_CATEGORY primary key nonclustered (CID)
)
go

/*==============================================================*/
/* Table: GAME                                                  */
/*==============================================================*/
create table GAME (
   GID                  int                  IDENTITY(1,1) not null,
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
   PAYMENT_NO           int                  IDENTITY(1,1) not null,
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
   UID                  int                  IDENTITY(1,1) not null,
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
   VID                  int                  IDENTITY(1,1) not null,
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

-- Insert into USER table without specifying IDs
INSERT INTO [USER] (USER_NAME, USER_EMAIL, USER_PHONE, JOIN_DATE) VALUES 
('Ahmed Mohamed', 'ahmed@example.com', '01012345678', '2023-01-15'),
('Fatima Ali', 'fatima@example.com', '01023456789', '2023-02-20'),
('Omar Hassan', 'omar@example.com', '01034567890', '2023-03-10'),
('Layla Ibrahim', 'layla@example.com', '01045678901', '2023-04-05'),
('Karim Mahmoud', 'karim@example.com', '01056789012', '2023-05-12');

-- Insert into CATEGORY table without specifying IDs
INSERT INTO CATEGORY (CATEGORY_NAME) VALUES
('Action'),
('Adventure'),
('Sports'),
('Strategy'),
('Puzzle');

-- Insert into VENDOR table without specifying IDs
INSERT INTO VENDOR (VENDOR_NAME, VENDOR_EMAIL, ADDRESS, VENDOR_PHONE) VALUES
('Cairo Games', 'cairo@games.com', 'Maadi, Cairo', '02025123456'),
('Alexandria Entertainment', 'alex@entertainment.com', 'Alexandria', '03425789012'),
('Delta Digital', 'delta@digital.com', 'Mansoura', '05025741236'),
('Upper Games', 'upper@games.com', 'Luxor', '09523698741');

-- Insert into ADMIN table without specifying IDs
INSERT INTO ADMIN (ADMIN_NAME, USER_PHONE, USER_EMAIL) VALUES
('Mahmoud Admin', '01112345678', 'mahmoud@admin.com'),
('Sara Admin', '01223456789', 'sara@admin.com');

-- Insert into GAME table without specifying IDs
-- Note: We need to reference the correct AID, VID, CID which are auto-generated
-- For simplicity in this script, we'll use variables to store these IDs
DECLARE @AdminID1 INT, @AdminID2 INT;
DECLARE @VendorID1 INT, @VendorID2 INT, @VendorID3 INT, @VendorID4 INT;
DECLARE @CategoryID1 INT, @CategoryID2 INT, @CategoryID3 INT, @CategoryID4 INT, @CategoryID5 INT;

-- Get the generated IDs
SELECT @AdminID1 = AID FROM ADMIN WHERE ADMIN_NAME = 'Mahmoud Admin';
SELECT @AdminID2 = AID FROM ADMIN WHERE ADMIN_NAME = 'Sara Admin';

SELECT @VendorID1 = VID FROM VENDOR WHERE VENDOR_NAME = 'Cairo Games';
SELECT @VendorID2 = VID FROM VENDOR WHERE VENDOR_NAME = 'Alexandria Entertainment';
SELECT @VendorID3 = VID FROM VENDOR WHERE VENDOR_NAME = 'Delta Digital';
SELECT @VendorID4 = VID FROM VENDOR WHERE VENDOR_NAME = 'Upper Games';

SELECT @CategoryID1 = CID FROM CATEGORY WHERE CATEGORY_NAME = 'Action';
SELECT @CategoryID2 = CID FROM CATEGORY WHERE CATEGORY_NAME = 'Adventure';
SELECT @CategoryID3 = CID FROM CATEGORY WHERE CATEGORY_NAME = 'Sports';
SELECT @CategoryID4 = CID FROM CATEGORY WHERE CATEGORY_NAME = 'Strategy';
SELECT @CategoryID5 = CID FROM CATEGORY WHERE CATEGORY_NAME = 'Puzzle';

-- Insert games using the fetched IDs
INSERT INTO GAME (AID, VID, CID, GAME_NAME, PRICE, RELEASE_DATE, DESCRIPTION) VALUES
(@AdminID1, @VendorID1, @CategoryID1, 'Desert Warriors', 49.99, '2023-03-15', 'Action game set in Egyptian deserts'),
(@AdminID1, @VendorID2, @CategoryID2, 'Pharaoh Tomb', 59.99, '2023-04-20', 'Adventure in ancient Egyptian pyramids'),
(@AdminID2, @VendorID3, @CategoryID3, 'Cairo Football 2024', 39.99, '2023-09-10', 'Football simulation with Egyptian teams'),
(@AdminID2, @VendorID4, @CategoryID4, 'Nile Empire', 29.99, '2023-07-05', 'Strategy game set along the Nile River'),
(@AdminID1, @VendorID1, @CategoryID5, 'Hieroglyphic Puzzles', 19.99, '2023-05-12', 'Puzzle game based on hieroglyphics'),
(@AdminID2, @VendorID2, @CategoryID1, 'Alexandria Rally', 34.99, '2023-08-18', 'Racing through streets of Alexandria'),
(@AdminID1, @VendorID3, @CategoryID2, 'Legends of Sinai', 54.99, '2023-06-22', 'RPG set in the Sinai Peninsula'),
(@AdminID2, @VendorID4, @CategoryID3, 'Hotel Tycoon Egypt', 24.99, '2023-10-30', 'Simulation game managing Egyptian resorts');

-- For RENTALS, REVIEWS, and PAYMENT tables, we need to reference the USER and GAME IDs
-- Let's create variables to store these IDs

DECLARE @UserID1 INT, @UserID2 INT, @UserID3 INT, @UserID4 INT, @UserID5 INT;
DECLARE @GameID1 INT, @GameID2 INT, @GameID3 INT, @GameID4 INT, @GameID5 INT, @GameID6 INT, @GameID7 INT, @GameID8 INT;

-- Get USER IDs
SELECT @UserID1 = UID FROM [USER] WHERE USER_NAME = 'Ahmed Mohamed';
SELECT @UserID2 = UID FROM [USER] WHERE USER_NAME = 'Fatima Ali';
SELECT @UserID3 = UID FROM [USER] WHERE USER_NAME = 'Omar Hassan';
SELECT @UserID4 = UID FROM [USER] WHERE USER_NAME = 'Layla Ibrahim';
SELECT @UserID5 = UID FROM [USER] WHERE USER_NAME = 'Karim Mahmoud';

-- Get GAME IDs
SELECT @GameID1 = GID FROM GAME WHERE GAME_NAME = 'Desert Warriors';
SELECT @GameID2 = GID FROM GAME WHERE GAME_NAME = 'Pharaoh Tomb';
SELECT @GameID3 = GID FROM GAME WHERE GAME_NAME = 'Cairo Football 2024';
SELECT @GameID4 = GID FROM GAME WHERE GAME_NAME = 'Nile Empire';
SELECT @GameID5 = GID FROM GAME WHERE GAME_NAME = 'Hieroglyphic Puzzles';
SELECT @GameID6 = GID FROM GAME WHERE GAME_NAME = 'Alexandria Rally';
SELECT @GameID7 = GID FROM GAME WHERE GAME_NAME = 'Legends of Sinai';
SELECT @GameID8 = GID FROM GAME WHERE GAME_NAME = 'Hotel Tycoon Egypt';

-- Insert into RENTALS table using the fetched IDs
INSERT INTO RENTALS (UID, GID, RENT_DATE, RETURN_DATE) VALUES
(@UserID1, @GameID1, '2025-04-01', '2025-04-07'),
(@UserID2, @GameID1, '2025-04-05', '2025-04-12'),
(@UserID3, @GameID1, '2025-04-10', '2025-04-17'),
(@UserID1, @GameID2, '2025-04-02', '2025-04-09'),
(@UserID2, @GameID2, '2025-04-12', '2025-04-19'),
(@UserID3, @GameID3, '2025-04-05', '2025-04-12'),
(@UserID4, @GameID3, '2025-04-18', '2025-04-25'),
(@UserID1, @GameID4, '2025-04-08', '2025-04-15'),
(@UserID2, @GameID5, '2025-04-10', '2025-04-17'),
(@UserID3, @GameID6, '2025-04-12', '2025-04-19'),
(@UserID1, @GameID7, '2025-04-22', '2025-04-29'),
(@UserID5, @GameID3, '2025-03-15', '2025-03-22'); -- A rental from previous month

-- Insert into REVIEWS table using the fetched IDs
INSERT INTO REVIEWS (UID, GID, REVIEW_CONTENT) VALUES
(@UserID1, @GameID1, 'Great action game with stunning desert graphics!'),
(@UserID2, @GameID1, 'Challenging gameplay but very enjoyable'),
(@UserID3, @GameID1, 'Love the Egyptian setting and characters'),
(@UserID1, @GameID2, 'Amazing adventure in the pyramids'),
(@UserID2, @GameID2, 'The puzzles are quite challenging'),
(@UserID3, @GameID3, 'Realistic football simulation with Egyptian teams'),
(@UserID4, @GameID3, 'Fun to play but needs more teams');

-- Insert into PAYMENT table using the fetched IDs
INSERT INTO PAYMENT (UID, GID, RENT_DATE, PAYMENT_DATE, AMOUNT) VALUES
(@UserID1, @GameID1, '2025-04-01', '2025-04-01', 10.00),
(@UserID2, @GameID1, '2025-04-05', '2025-04-05', 10.00),
(@UserID3, @GameID1, '2025-04-10', '2025-04-10', 10.00),
(@UserID1, @GameID2, '2025-04-02', '2025-04-02', 12.00),
(@UserID2, @GameID2, '2025-04-12', '2025-04-12', 12.00),
(@UserID3, @GameID3, '2025-04-05', '2025-04-05', 8.00),
(@UserID4, @GameID3, '2025-04-18', '2025-04-18', 8.00),
(@UserID1, @GameID4, '2025-04-08', '2025-04-08', 7.50),
(@UserID2, @GameID5, '2025-04-10', '2025-04-10', 5.00),
(@UserID3, @GameID6, '2025-04-12', '2025-04-12', 7.00),
(@UserID1, @GameID7, '2025-04-22', '2025-04-22', 11.00),
(@UserID5, @GameID3, '2025-03-15', '2025-03-15', 8.00);