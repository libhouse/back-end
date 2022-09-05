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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301010331_Initial')
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [Name] varchar(40) NOT NULL,
        [LastName] varchar(40) NOT NULL,
        [BirthDate] date NOT NULL,
        [Gender] varchar(11) NOT NULL,
        [Phone] char(11) NOT NULL,
        [Email] varchar(60) NOT NULL,
        [Cpf] char(11) NOT NULL,
        [UserType] varchar(8) NOT NULL,
        [Active] bit NOT NULL DEFAULT (1),
        [CreatedAt] datetime NOT NULL DEFAULT (GETDATE()),
        [UpdatedAt] datetime NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301010331_Initial')
BEGIN
    CREATE TABLE [Owners] (
        [Id] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Owners] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Owners_Users_Id] FOREIGN KEY ([Id]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301010331_Initial')
BEGIN
    CREATE TABLE [Residents] (
        [Id] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Residents] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Residents_Users_Id] FOREIGN KEY ([Id]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301010331_Initial')
BEGIN
    CREATE UNIQUE INDEX [idx_user_email] ON [Users] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301010331_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301010331_Initial', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220410183026_UserActiveDefaultValue')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Active');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220410183026_UserActiveDefaultValue')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220410183026_UserActiveDefaultValue', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812193417_CustomPhoneEmailTypes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220812193417_CustomPhoneEmailTypes', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904184659_Schema')
BEGIN
    IF SCHEMA_ID(N'Business') IS NULL EXEC(N'CREATE SCHEMA [Business];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904184659_Schema')
BEGIN
    ALTER SCHEMA [Business] TRANSFER [Users];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904184659_Schema')
BEGIN
    ALTER SCHEMA [Business] TRANSFER [Residents];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904184659_Schema')
BEGIN
    ALTER SCHEMA [Business] TRANSFER [Owners];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220904184659_Schema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220904184659_Schema', N'5.0.0');
END;
GO

COMMIT;
GO

