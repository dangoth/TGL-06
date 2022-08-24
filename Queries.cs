using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExample;
public class Queries
{
    public const string CreatePuppiesTableQuery = $@"
    CREATE TABLE [dbo].[Puppies](
    [Id] [int] NOT NULL IDENTITY(1, 1),
    [Name] [nvarchar](max) NOT NULL,
    [BirthDate] [datetime] NOT NULL,
    [PuppyLengthInCm] [float] NOT NULL,
    [IsCute] [bit] NOT NULL,
    [IsGoodPuppy] [bit] NOT NULL,
    CONSTRAINT [PK_Puppies] PRIMARY KEY CLUSTERED
    (
        [Id] ASC
    ) ON [PRIMARY]
    )";
    public const string CheckIfTableExistsQuery = $@"
    IF (EXISTS (SELECT *
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = 'dbo'
        AND TABLE_NAME = @TableName))
    SELECT 1
    ELSE SELECT 0
    ";
    public const string InsertPuppiesDataQuery = $@"
    INSERT INTO [dbo].[Puppies]
    (Name, BirthDate, PuppyLengthInCm, IsCute, IsGoodPuppy)
    VALUES (@Name, @BirthDate, @PuppyLengthInCm, @IsCute, @IsGoodPuppy)
    ";
    public const string DeletePuppiesTableQuery = $@"
    DROP TABLE [dbo].[Puppies]
    ";
    public const string CountPuppiesTableQuery = $@"
    SELECT COUNT(1)
    FROM [dbo].[Puppies]
    ";
    public const string DoggosTablesCreationScript = $@"
    CREATE TABLE [dbo].[Doggos](
    [Id] [int] NOT NULL IDENTITY(1, 1),
    [Name] [nvarchar](300) NOT NULL,
    [DateOfBirth] [datetime] NOT NULL,
    [Photo] [binary] NOT NULL,
    Height [float] NOT NULL,
    Weight [float] NOT NULL,
    CONSTRAINT [PK_Doggos] PRIMARY KEY CLUSTERED
    (
        [Id] ASC
    ) ON [PRIMARY]
    )

    CREATE TABLE [dbo].[DoggoInfos](
    [Id] [int] NOT NULL IDENTITY(1, 1),
    [OwnerName] [nvarchar](300) NOT NULL,
    [Age] [int] NOT NULL,
    [DoggoId] [int] NOT NULL,
    CONSTRAINT [PK_Doggos] PRIMARY KEY CLUSTERED
    (
        [Id] ASC
    ) ON [PRIMARY]
    FOREIGN KEY (DoggoId) REFERENCES Doggos(Id)
    )";
}
