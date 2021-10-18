CREATE TABLE FitnessInformation
(
    FitnessName varchar(50) PRIMARY KEY CLUSTERED,
    Address varchar(50)
)

CREATE TABLE ClassInformation
(
    ClassId uniqueidentifier PRIMARY KEY CLUSTERED,
    FitnessName varchar(50),
    ClassName varchar(50),
    ClassType varchar(50),
    IsClassFull BIT,
    MaxParticipants int,
    NumberOfParticipants int,
    ClassTimeStamp varchar(50),
    FOREIGN KEY (FitnessName) REFERENCES FitnessInformation(FitnessName)
)