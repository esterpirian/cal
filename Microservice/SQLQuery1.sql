Create table  Customer
(
   UserId int Not null ,
    FirstName  varchar(50) collate Hebrew_100_BIN2,
     LastName  varchar(50) collate Hebrew_100_BIN2,
     IdNum varchar(50),
     CustomerGuid varchar(36),
      EmailAdd varchar(50)
      CONSTRAINT column_EmailAdd
CHECK
(
EmailAdd not like '%^[A-Za-z0-9._+%-]+@[A-Za-z0-9.-]+[.][A-Za-z]+$%' 

),
    CONSTRAINT column_FirstName
CHECK
(
FirstName not like N'%[^א-תa-z]%'
)
    ,
    CONSTRAINT column_LastName
CHECK
(
LastName not like N'%[^א-תa-z ]%'

)
    ,
     CONSTRAINT column_IdNum
CHECK
(
IdNum not like '%[^0-9]%' and LEN(IdNum) =9
)
    ,
     CONSTRAINT column_CustomerGuid
CHECK
(
CustomerGuid not like '%[^0-9a-z-]%' and LEN(CustomerGuid) =36

)
    ,
     
 PRIMARY KEY (UserId)
    
  
)
INSERT INTO Customer(FirstName,LastName,IdNum,CustomerGuid,EmailAdd,UserId) values(N'אסתרp',N'pirian דניאל','326132941','0f8fad5b-d9cb-469f-a165-70867728950j','hju@pp.com',3)
--drop table Customer
Create table  BankAccounts
(
    UserId int ,
    BranchCode int,
    BankAccountNumber  varchar(50)
    CONSTRAINT column_BankAccountNumber
CHECK
(
  BankAccountNumber not like '%[^0-9]%' and LEN(BankAccountNumber) >15
)
       ,
    FOREIGN KEY (UserId) REFERENCES Customer(UserId)
    ,
    FOREIGN KEY (BranchCode) REFERENCES BankBranches(BankBranch)
  
)
--drop table BankAccounts
insert into BankAccounts(UserId,BranchCode,BankAccountNumber) values(3,112,'1234567891458964')
insert into BankAccounts(UserId,BranchCode,BankAccountNumber) values(3,111,'1234567891458111')
