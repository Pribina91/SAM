---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  File create... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  /* SQLINES EVALUATION VERSION TRUNCATES VARIABLE NAMES AND COMMENTS. */
  /* OBTAIN A LICENSE AT WWW.SQLINES.COM FOR FULL CONVERSION. THANK YOU. */

  CREATE TABLE "ASFEU"."BD__CISELNIKY" 
   (	"KOD" FLOAT, 
	"NADRAD" FLOAT, 
	"NAZOV" VARCHAR(70), 
	"UZIV_IDENT" VARCHAR(100)
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_MER_CASOVE_RADY" 
   (	"DIAGRAM_ID" FLOAT, 
	"CAS" BIGINT, 
	"MNOZSTVO" FLOAT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_MER_DIAGRAMY" 
   (	"KOD" FLOAT, 
	"OOM_ID" FLOAT, 
	"DATUM" DATETIME, 
	"JEDNOTKA" FLOAT, 
	"DRUH_MERANIA" FLOAT, 
	"SUMARNE_MNOZSTVO" FLOAT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" 
   (	"KOD" FLOAT, 
	"OOM_ID" FLOAT, 
	"MNOZSTVO" FLOAT, 
	"JEDNOTKA" FLOAT, 
	"DATUM_OD" DATETIME, 
	"DATUM_DO" DATETIME, 
	"DRUH_MERANIA" FLOAT, 
	"TYP_ODPOCTU" FLOAT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_OOM" 
   (	"KOD" FLOAT, 
	"DRUH" FLOAT, 
	"SUSTAVA_ID" FLOAT, 
	"PSC" VARCHAR(10), 
	"TYP_MERANIA" FLOAT, 
	"TRIEDA_TDO" FLOAT, 
	"NAPATOVA_UROVEN" FLOAT, 
	"ROCNY_PREDPOKLAD" DECIMAL(16,6), 
	"VYROBNA_ID" FLOAT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_SUSTAVY" 
   (	"KOD" FLOAT, 
	"TYP_SUSTAVY" FLOAT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_TDO_NORM" 
   (	"KOD" FLOAT, 
	"TRIEDA_TDO" FLOAT, 
	"TYP_TDO" FLOAT, 
	"ROK" BIGINT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_TDO_NORM_CASOVE_RADY" 
   (	"DIAGRAM_ID" FLOAT, 
	"CAS" BIGINT, 
	"MNOZSTVO" FLOAT
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Tab... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE TABLE "ASFEU"."BD_TDO_NORM_DIAGRAMY" 
   (	"KOD" FLOAT, 
	"NORM_TDO_ID" FLOAT, 
	"DATUM" DATETIME
   ) ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD__CISELNIKY_PK" ON "ASFEU"."BD__CISELNIKY" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_MER_CASOVE_RADY_PK" ON "ASFEU"."BD_MER_CASOVE_RADY" ("DIAGRAM_ID", "CAS") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_MER_DIAGRAMY_PK" ON "ASFEU"."BD_MER_DIAGRAMY" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_MER_NEPRIEBEHOVE_PK" ON "ASFEU"."BD_MER_NEPRIEBEHOVE" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_OOM_PK" ON "ASFEU"."BD_OOM" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_SUSTAVY_PK" ON "ASFEU"."BD_SUSTAVY" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_TDO_NORM_PK" ON "ASFEU"."BD_TDO_NORM" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_TDO_NORM_CASOVE_RADY_PK" ON "ASFEU"."BD_TDO_NORM_CASOVE_RADY" ("DIAGRAM_ID", "CAS") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  DDL for Ind... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  CREATE UNIQUE INDEX "ASFEU"."BD_TDO_NORM_DIAGRAMY_PK" ON "ASFEU"."BD_TDO_NORM_DIAGRAMY" ("KOD") 
  ;
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD__CISELNIKY" MODIFY ("UZIV_IDENT" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD__CISELNIKY" MODIFY ("NAZOV" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD__CISELNIKY" MODIFY ("NADRAD" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD__CISELNIKY" ADD CONSTRAINT "BD__CISELNIKY_PK" PRIMARY KEY ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_MER_CASOVE_RADY" MODIFY ("MNOZSTVO" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_CASOVE_RADY" ADD CONSTRAINT "BD_MER_CASOVE_RADY_PK" PRIMARY KEY ("DIAGRAM_ID", "CAS");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" MODIFY ("SUMARNE_MNOZSTVO" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" MODIFY ("DRUH_MERANIA" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" MODIFY ("JEDNOTKA" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" MODIFY ("DATUM" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" MODIFY ("OOM_ID" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" ADD CONSTRAINT "BD_MER_DIAGRAMY_PK" PRIMARY KEY ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" MODIFY ("MNOZSTVO" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" MODIFY ("DRUH_MERANIA" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" MODIFY ("DATUM_DO" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" MODIFY ("DATUM_OD" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" MODIFY ("JEDNOTKA" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" MODIFY ("OOM_ID" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" ADD CONSTRAINT "BD_MER_NEPRIEBEHOVE_PK" PRIMARY KEY ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_OOM" ADD CONSTRAINT "BD_OOM_PK" PRIMARY KEY ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_SUSTAVY" MODIFY ("TYP_SUSTAVY" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_SUSTAVY" ADD CONSTRAINT "BD_SUSTAVY_PK" PRIMARY KEY ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_TDO_NORM" MODIFY ("ROK" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_TDO_NORM" MODIFY ("TYP_TDO" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_TDO_NORM" ADD CONSTRAINT "BD_TDO_NORM_PK" PRIMARY KEY ("KOD");
  ALTER TABLE "ASFEU"."BD_TDO_NORM" MODIFY ("TRIEDA_TDO" NOT NULL ENABLE);
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_TDO_NORM_CASOVE_RADY" MODIFY ("MNOZSTVO" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_TDO_NORM_CASOVE_RADY" ADD CONSTRAINT "BD_TDO_NORM_CASOVE_RADY_PK" PRIMARY KEY ("DIAGRAM_ID", "CAS");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Constraints... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_TDO_NORM_DIAGRAMY" ADD CONSTRAINT "BD_TDO_NORM_DIAGRAMY_PK" PRIMARY KEY ("KOD");
  ALTER TABLE "ASFEU"."BD_TDO_NORM_DIAGRAMY" MODIFY ("DATUM" NOT NULL ENABLE);
  ALTER TABLE "ASFEU"."BD_TDO_NORM_DIAGRAMY" MODIFY ("NORM_TDO_ID" NOT NULL ENABLE);
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_MER_CASOVE_RADY" ADD CONSTRAINT "BD_MER_CASOVE_RADY_DIAGRAM" FOREIGN KEY ("DIAGRAM_ID")
	  REFERENCES "ASFEU"."BD_MER_DIAGRAMY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" ADD CONSTRAINT "BD_MER_DIAGRAMY_BD_OOM_FK1" FOREIGN KEY ("OOM_ID")
	  REFERENCES "ASFEU"."BD_OOM" ("KOD");
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" ADD CONSTRAINT "BD_MER_DIAGRAMY_BD__CISEL_FK1" FOREIGN KEY ("JEDNOTKA")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
  ALTER TABLE "ASFEU"."BD_MER_DIAGRAMY" ADD CONSTRAINT "BD_MER_DIAGRAMY_BD__CISEL_FK2" FOREIGN KEY ("DRUH_MERANIA")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" ADD CONSTRAINT "BD_MER_NEPRIEBEHOVE_BD_OO_FK1" FOREIGN KEY ("OOM_ID")
	  REFERENCES "ASFEU"."BD_OOM" ("KOD");
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" ADD CONSTRAINT "BD_MER_NEPRIEBEHOVE_BD__C_FK1" FOREIGN KEY ("DRUH_MERANIA")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
  ALTER TABLE "ASFEU"."BD_MER_NEPRIEBEHOVE" ADD CONSTRAINT "BD_MER_NEPRIEBEHOVE_BD__C_FK2" FOREIGN KEY ("TYP_ODPOCTU")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_OOM" ADD CONSTRAINT "BD_OOM_BD_SUSTAVY_FK1" FOREIGN KEY ("SUSTAVA_ID")
	  REFERENCES "ASFEU"."BD_SUSTAVY" ("KOD");
  ALTER TABLE "ASFEU"."BD_OOM" ADD CONSTRAINT "BD_OOM_BD__CISELNIKY_FK1" FOREIGN KEY ("DRUH")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
  ALTER TABLE "ASFEU"."BD_OOM" ADD CONSTRAINT "BD_OOM_BD__CISELNIKY_FK2" FOREIGN KEY ("TYP_MERANIA")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
  ALTER TABLE "ASFEU"."BD_OOM" ADD CONSTRAINT "BD_OOM_BD__CISELNIKY_FK3" FOREIGN KEY ("NAPATOVA_UROVEN")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_SUSTAVY" ADD CONSTRAINT "BD_SUSTAVY_BD__CISELNIKY_FK1" FOREIGN KEY ("TYP_SUSTAVY")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_TDO_NORM" ADD CONSTRAINT "BD_TDO_NORM_BD__CISELNIKY_FK1" FOREIGN KEY ("TYP_TDO")
	  REFERENCES "ASFEU"."BD__CISELNIKY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_TDO_NORM_CASOVE_RADY" ADD CONSTRAINT "BD_TDO_NORM_CASOVE_RADY_B_FK1" FOREIGN KEY ("DIAGRAM_ID")
	  REFERENCES "ASFEU"."BD_TDO_NORM_DIAGRAMY" ("KOD");
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 
--  Ref Constra... *** SQLINES FOR EVALUATION USE ONLY *** 
---------------... *** SQLINES FOR EVALUATION USE ONLY *** 

  ALTER TABLE "ASFEU"."BD_TDO_NORM_DIAGRAMY" ADD CONSTRAINT "BD_TDO_NORM_DIAGRAMY_BD_T_FK1" FOREIGN KEY ("NORM_TDO_ID")
	  REFERENCES "ASFEU"."BD_TDO_NORM" ("KOD");

