﻿
-- Insert data into the 'Country' table for the United Kingdom
INSERT INTO Country VALUES ('United Kingdom', 1, 0,'20120618 10:34:09 AM');
INSERT INTO State VALUES (1, 'London', 1, 0, '20120618 10:34:09 AM');
INSERT INTO State VALUES (1, 'Manchester', 1, 0, '20120618 10:34:09 AM');
INSERT INTO State VALUES (1,'Birmingham', 1, 0, '20120618 10:34:09 AM');
-- Insert more regions as needed
INSERT INTO AspNetRoles VALUES (NewId(),'SuperAdmin','SuperAdmin',NEWID());
INSERT INTO AspNetRoles VALUES (NewId(),'CompanyAdmin','CompanyAdmin',NEWID());
INSERT INTO AspNetRoles VALUES (NewId(),'CompanyStaff','CompanyStaff',NEWID());
INSERT INTO AspNetRoles VALUES (NewId(),'User','User',NEWID());