IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    IF SCHEMA_ID(N'Authentication') IS NULL EXEC(N'CREATE SCHEMA [Authentication];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Authentication].[AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetRefreshTokens] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Token] char(71) NOT NULL,
        [JwtId] varchar(100) NOT NULL,
        [IsUsed] bit NOT NULL DEFAULT (0),
        [IsRevoked] bit NOT NULL DEFAULT (0),
        [CreatedAt] datetime NOT NULL DEFAULT (GETDATE()),
        [RevokedAt] datetime NULL,
        [ExpiresIn] datetime NOT NULL,
        CONSTRAINT [PK_AspNetRefreshTokens] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRefreshTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Authentication].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Authentication].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Authentication].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Authentication].[AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Authentication].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE TABLE [Authentication].[AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Authentication].[AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Authentication].[AspNetRoles]'))
        SET IDENTITY_INSERT [Authentication].[AspNetRoles] ON;
    EXEC(N'INSERT INTO [Authentication].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''8e5a2540-47e8-4097-90e6-5a6f955ab330'', N''25dc77e2-1847-4751-9761-62e00971da90'', N''User'', N''USER'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Authentication].[AspNetRoles]'))
        SET IDENTITY_INSERT [Authentication].[AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Authentication].[AspNetRoles]'))
        SET IDENTITY_INSERT [Authentication].[AspNetRoles] ON;
    EXEC(N'INSERT INTO [Authentication].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''0b6f46eb-209a-4e6a-b96f-1152b7f8742c'', N''42702276-15bb-4590-a849-aec0453009c5'', N''Resident'', N''RESIDENT'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Authentication].[AspNetRoles]'))
        SET IDENTITY_INSERT [Authentication].[AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Authentication].[AspNetRoles]'))
        SET IDENTITY_INSERT [Authentication].[AspNetRoles] ON;
    EXEC(N'INSERT INTO [Authentication].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''20746a1b-4ed1-4a6f-82ed-7b4d0f363382'', N''9ff00118-c73e-45ef-a2b7-872446e14d7b'', N''Owner'', N''OWNER'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[Authentication].[AspNetRoles]'))
        SET IDENTITY_INSERT [Authentication].[AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE UNIQUE INDEX [idx_jwt_id] ON [Authentication].[AspNetRefreshTokens] ([JwtId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE UNIQUE INDEX [idx_refresh_token] ON [Authentication].[AspNetRefreshTokens] ([Token]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE INDEX [IX_AspNetRefreshTokens_UserId] ON [Authentication].[AspNetRefreshTokens] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [Authentication].[AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Authentication].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [Authentication].[AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [Authentication].[AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [Authentication].[AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    CREATE INDEX [EmailIndex] ON [Authentication].[AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Authentication].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904195846_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220904195846_Initial', N'5.0.0');
END;
GO

COMMIT;
GO

