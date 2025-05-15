declare @username nvarchar(100);
declare @password nvarchar(100);
declare @name nvarchar(100);
declare @email nvarchar(100);
declare @userphone nvarchar(20);
declare @joindate date;
declare @aid int;
declare @vid int;
declare @cid int;
declare @desc nvarchar(max);
declare @price decimal(10, 2);
declare @date date;
declare @gamename nvarchar(100);
declare @gid int;
declare @userid int; 
declare @rentdate date;
declare @returndate date;
declare @paymentdate date;
declare @amount decimal(10, 2);

-- =============================================
-- sign up / login
-- =============================================

-- check if user exists (admin or user)
select count(*) from admin where admin_name = @username;
-- or
select count(*) from [user] where user_name = @username;

-- create new admin account
insert into admin (admin_name, user_email, user_phone, password) 
values (@name, @email, @userphone, @password);

-- create new user account
insert into [user] (user_name, user_email, user_phone, join_date, password) 
values (@username, @email, @userphone, @joindate, @password);

-- admin authentication
select count(*) from admin where admin_name = @username and password = @password;

-- user authentication
select count(*) from [user] where user_name = @username and password = @password;

-- =============================================
-- Admin dashboard
-- =============================================

-- check if vendor exists
select count(*) from vendor where vid = @vid;

-- check if category exists
select count(*) from category where cid = @cid;

-- add new game
insert into game (aid, vid, cid, game_name, price, release_date, description) 
values (@aid, @vid, @cid, @gamename, @price, @date, @desc);

-- update game description
update game set description = @desc where game_name = @gamename;

-- delete game
delete from game where game_name = @gamename;

-- =============================================
-- section 3: user dashboard 
-- =============================================

-- get user id from username
select uid from [user] where user_name = @username;

-- update user password
update [user] set password = @password where uid = @userid;

-- load all games
select 
    gid as game_id, 
    game_name, 
    price, 
    release_date, 
    description 
from game;

-- get game id by name
select gid from game where game_name = @gamename;

-- get game price
select price from game where gid = @gid;

-- get current user rentals
select 
    r.gid as game_id, 
    g.game_name, 
    r.rent_date, 
    r.return_date 
from rentals r 
join game g on r.gid = g.gid 
where r.uid = @userid;  

-- create new rental
insert into rentals (uid, gid, rent_date, return_date) 
values (@userid, @gid, @rentdate, @returndate);

-- create payment record
insert into payment (uid, gid, rent_date, payment_date, amount) 
values (@userid, @gid, @rentdate, @paymentdate, @amount);

-- delete user's payment records
delete from payment where uid = @userid;

-- delete user's rental records
delete from rentals where uid = @userid;

-- =============================================
-- analytical reports
-- =============================================

-- most popular game
SELECT TOP 1 with ties
  g.GAME_NAME AS [Game], 
  COUNT(DISTINCT r.UID) AS [Renters] 
FROM GAME g 
JOIN RENTALS r ON g.GID=r.GID 
GROUP BY g.GAME_NAME 
ORDER BY Renters DESC

-- games not rented last month
select g.game_name as game
from game g
where g.gid not in (
    select distinct gid
    from rentals
    where rent_date >= dateadd(month, -1, getdate())
);

----------------------------
-- Clients with most rentals last month
-----------------------------
SELECT TOP 1 with ties 
  u.USER_NAME AS [Client], 
  COUNT(r.GID) AS [Rentals] 
FROM [USER] u 
JOIN RENTALS r ON u.UID=r.UID 
WHERE r.RENT_DATE >= DATEADD(MONTH, -1, GETDATE()) 
GROUP BY u.USER_NAME 
ORDER BY Rentals DESC 

-----------------------------
-- Vendors with most rented games last month
-----------------------------
SELECT TOP 1 with ties
  v.VENDOR_NAME AS [Vendor], 
  COUNT(r.GID) AS [Rentals] 
FROM VENDOR v 
JOIN GAME g ON v.VID=g.VID 
JOIN RENTALS r ON g.GID=r.GID 
WHERE r.RENT_DATE >= DATEADD(MONTH, -1, GETDATE()) 
GROUP BY v.VENDOR_NAME 
ORDER BY Rentals DESC ;

-----------------------------
-- Vendors whose games had no renting last month
-----------------------------
SELECT 
    v.VENDOR_NAME AS [Vendor]
FROM VENDOR v
WHERE v.VID NOT IN (
    SELECT DISTINCT g.VID
    FROM GAME g
    JOIN RENTALS r ON g.GID = r.GID
    WHERE r.RENT_DATE >= DATEADD(MONTH, -1, GETDATE())
);

-----------------------------
-- Vendors who didn't add any game last year
-----------------------------
SELECT 
    v.VENDOR_NAME AS [Vendor]
FROM VENDOR v
WHERE v.VID NOT IN (
    SELECT DISTINCT VID
    FROM GAME
    WHERE RELEASE_DATE >= DATEADD(YEAR, -1, GETDATE())
);
