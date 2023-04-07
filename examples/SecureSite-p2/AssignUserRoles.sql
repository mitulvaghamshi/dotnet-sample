USE [SecureSite];

INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Admin');
INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Manager');
INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Clerk');

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'admin@email.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Admin')
);

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'admin@email.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Manager')
);

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'manager@email.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Manager')
);

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'clerk@email.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Clerk')
);

SELECT u.Email, r.[Name] [Role] FROM AspNetUserRoles ur
JOIN AspNetRoles r ON ur.RoleId = r.Id
JOIN AspNetUsers u ON ur.UserId = u.Id
