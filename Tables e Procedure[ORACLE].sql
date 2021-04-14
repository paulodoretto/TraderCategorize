CREATE TABLE CATEGORY (
       Id                  Decimal NOT NULL PRIMARY KEY,
       Name                Varchar2(20) NOT NULL,
       ValueInitial        Decimal(12,2) NOT NULL,
       ValueFinal          Decimal(12,2) NOT NULL,
       SectorClient        Decimal(1) NOT NULL);
/
CREATE OR REPLACE PROCEDURE DOT_CATEGORY_ADD(pId             In CATEGORY.Id%Type,
                                             pName           In CATEGORY.Name%Type,
                                             pValueInitial   in CATEGORY.ValueInitial%Type,
                                             pValueFinal     in CATEGORY.ValueFinal%Type,
                                             pSectorClient   In CATEGORY.SectorClient%Type) as 

   tCategory        CATEGORY%rowtype;                                           
Begin
   tCategory.Id := pId;
   tCategory.Name := pName;
   tCategory.ValueInitial := pValueInitial;   
   tCategory.ValueFinal := pValueFinal;      
   tCategory.SectorClient := pSectorClient;
                
   INSERT INTO CATEGORY VALUES tCategory;
END DOT_CATEGORY_ADD;                                             
/
CREATE OR REPLACE PROCEDURE DOT_CATEGORY_UPDATE(pId             In CATEGORY.Id%Type,
                                                pName           In CATEGORY.Name%Type,
                                                pValueInitial   in CATEGORY.ValueInitial%Type,
                                                pValueFinal     in CATEGORY.ValueFinal%Type,
                                                pSectorClient   In CATEGORY.SectorClient%Type) as 

   tCategory        CATEGORY%rowtype;                                           
Begin
   tCategory.Id := pId;
   tCategory.Name := pName;
   tCategory.ValueInitial := pValueInitial;   
   tCategory.ValueFinal := pValueFinal;      
   tCategory.SectorClient := pSectorClient;          
                
   UPDATE CATEGORY SET ROW = tCategory WHERE Id = tCategory.Id;
END DOT_CATEGORY_UPDATE;                                             
/
CREATE OR REPLACE PROCEDURE DOT_CATEGORY_DELETE(pId             In CATEGORY.Id%Type) as
                                           
Begin

   DELETE FROM CATEGORY WHERE Id = pId;
   
END DOT_CATEGORY_DELETE;
/
CREATE OR REPLACE TYPE "DOT_TRADERROW" as OBJECT
(
  Value      Decimal(12,2),
  SectorClient Decimal(1)
)
/
CREATE OR REPLACE TYPE "DOT_TRADER" AS TABLE OF DOT_TRADERROW;
/
CREATE OR REPLACE FUNCTION DOT_CATEGORIZE(pTypeTrader In DOT_TRADER) Return String is

   vsDescription            Varchar2(100);                                           
Begin

    For ni IN 1..pTypeTrader.COUNT LOOP
       
        For x in (Select C.Name, C.ValueInitial, C.ValueFinal, C.SectorClient from Category C) Loop
            
             if pTypeTrader(ni).Value >= x.ValueInitial and pTypeTrader(ni).Value <= x.ValueFinal and pTypeTrader(ni).SectorClient = x.SectorClient then
                
                if Length(vsDescription) > 0 then
                   vsDescription := vsDescription || ', ';
                 End If;
                        
                 vsDescription := vsDescription || x.Name;
              End If;      
        
        End Loop;
        
       
    End Loop;
    
    return vsDescription;
END DOT_CATEGORIZE;
