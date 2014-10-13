  select KOD from BD__CISELNIKY;
--------------------------------------------------------
--  DDL for Index BD__CISELNIKY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD__CISELNIKY_PK ON BD__CISELNIKY (KOD) 
  ;
--------------------------------------------------------
--  DDL for Index BD_MER_CASOVE_RADY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_MER_CASOVE_RADY_PK ON BD_MER_CASOVE_RADY (DIAGRAM_ID, CAS) 
  ;
--------------------------------------------------------
--  DDL for Index BD_MER_DIAGRAMY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_MER_DIAGRAMY_PK ON BD_MER_DIAGRAMY (KOD) 
  ;
--------------------------------------------------------
--  DDL for Index BD_MER_NEPRIEBEHOVE_PK
--------------------------------------------------------
--TODO
  CREATE UNIQUE INDEX BD_MER_NEPRIEBEHOVE_PK ON BD_MER_NEPRIEBEHOVE (KOD) 
  ;
--------------------------------------------------------
--  DDL for Index BD_OOM_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_OOM_PK ON BD_OOM (KOD) 
  ;
--------------------------------------------------------
--  DDL for Index BD_SUSTAVY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_SUSTAVY_PK ON BD_SUSTAVY (KOD) 
  ;
--------------------------------------------------------
--  DDL for Index BD_TDO_NORM_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_TDO_NORM_PK ON BD_TDO_NORM (KOD) 
  ;
--------------------------------------------------------
--  DDL for Index BD_TDO_NORM_CASOVE_RADY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_TDO_NORM_CASOVE_RADY_PK ON BD_TDO_NORM_CASOVE_RADY (DIAGRAM_ID, CAS) 
  ;
--------------------------------------------------------
--  DDL for Index BD_TDO_NORM_DIAGRAMY_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX BD_TDO_NORM_DIAGRAMY_PK ON BD_TDO_NORM_DIAGRAMY (KOD) 
  ;
--------------------------------------------------------
--  Constraints for Table BD__CISELNIKY
--------------------------------------------------------

  ALTER TABLE BD__CISELNIKY alter column UZIV_IDENT  NOT NULL  ;
  ALTER TABLE BD__CISELNIKY alter column NAZOV NOT NULL ;
  ALTER TABLE BD__CISELNIKY alter column NADRAD NOT NULL ;
  ALTER TABLE BD__CISELNIKY ADD CONSTRAINT BD__CISELNIKY_PK PRIMARY KEY (KOD) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_MER_CASOVE_RADY
--------------------------------------------------------

  ALTER TABLE BD_MER_CASOVE_RADY MODIFY (MNOZSTVO NOT NULL ENABLE);
  ALTER TABLE BD_MER_CASOVE_RADY ADD CONSTRAINT BD_MER_CASOVE_RADY_PK PRIMARY KEY (DIAGRAM_ID, CAS) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_MER_DIAGRAMY
--------------------------------------------------------

  ALTER TABLE BD_MER_DIAGRAMY MODIFY (SUMARNE_MNOZSTVO NOT NULL ENABLE);
  ALTER TABLE BD_MER_DIAGRAMY MODIFY (DRUH_MERANIA NOT NULL ENABLE);
  ALTER TABLE BD_MER_DIAGRAMY MODIFY (JEDNOTKA NOT NULL ENABLE);
  ALTER TABLE BD_MER_DIAGRAMY MODIFY (DATUM NOT NULL ENABLE);
  ALTER TABLE BD_MER_DIAGRAMY MODIFY (OOM_ID NOT NULL ENABLE);
  ALTER TABLE BD_MER_DIAGRAMY ADD CONSTRAINT BD_MER_DIAGRAMY_PK PRIMARY KEY (KOD) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_MER_NEPRIEBEHOVE
--------------------------------------------------------

  ALTER TABLE BD_MER_NEPRIEBEHOVE MODIFY (MNOZSTVO NOT NULL ENABLE);
  ALTER TABLE BD_MER_NEPRIEBEHOVE MODIFY (DRUH_MERANIA NOT NULL ENABLE);
  ALTER TABLE BD_MER_NEPRIEBEHOVE MODIFY (DATUM_DO NOT NULL ENABLE);
  ALTER TABLE BD_MER_NEPRIEBEHOVE MODIFY (DATUM_OD NOT NULL ENABLE);
  ALTER TABLE BD_MER_NEPRIEBEHOVE MODIFY (JEDNOTKA NOT NULL ENABLE);
  ALTER TABLE BD_MER_NEPRIEBEHOVE MODIFY (OOM_ID NOT NULL ENABLE);
  ALTER TABLE BD_MER_NEPRIEBEHOVE ADD CONSTRAINT BD_MER_NEPRIEBEHOVE_PK PRIMARY KEY (KOD) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_OOM
--------------------------------------------------------

  ALTER TABLE BD_OOM ADD CONSTRAINT BD_OOM_PK PRIMARY KEY (KOD) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_SUSTAVY
--------------------------------------------------------

  ALTER TABLE BD_SUSTAVY MODIFY (TYP_SUSTAVY NOT NULL ENABLE);
  ALTER TABLE BD_SUSTAVY ADD CONSTRAINT BD_SUSTAVY_PK PRIMARY KEY (KOD) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_TDO_NORM
--------------------------------------------------------

  ALTER TABLE BD_TDO_NORM MODIFY (ROK NOT NULL ENABLE);
  ALTER TABLE BD_TDO_NORM MODIFY (TYP_TDO NOT NULL ENABLE);
  ALTER TABLE BD_TDO_NORM ADD CONSTRAINT BD_TDO_NORM_PK PRIMARY KEY (KOD) ENABLE;
  ALTER TABLE BD_TDO_NORM MODIFY (TRIEDA_TDO NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table BD_TDO_NORM_CASOVE_RADY
--------------------------------------------------------

  ALTER TABLE BD_TDO_NORM_CASOVE_RADY MODIFY (MNOZSTVO NOT NULL ENABLE);
  ALTER TABLE BD_TDO_NORM_CASOVE_RADY ADD CONSTRAINT BD_TDO_NORM_CASOVE_RADY_PK PRIMARY KEY (DIAGRAM_ID, CAS) ENABLE;
--------------------------------------------------------
--  Constraints for Table BD_TDO_NORM_DIAGRAMY
--------------------------------------------------------

  ALTER TABLE BD_TDO_NORM_DIAGRAMY ADD CONSTRAINT BD_TDO_NORM_DIAGRAMY_PK PRIMARY KEY (KOD) ENABLE;
  ALTER TABLE BD_TDO_NORM_DIAGRAMY MODIFY (DATUM NOT NULL ENABLE);
  ALTER TABLE BD_TDO_NORM_DIAGRAMY MODIFY (NORM_TDO_ID NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table BD_MER_CASOVE_RADY
--------------------------------------------------------

  ALTER TABLE BD_MER_CASOVE_RADY ADD CONSTRAINT BD_MER_CASOVE_RADY_DIAGRAM FOREIGN KEY (DIAGRAM_ID)
	  REFERENCES BD_MER_DIAGRAMY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_MER_DIAGRAMY
--------------------------------------------------------

  ALTER TABLE BD_MER_DIAGRAMY ADD CONSTRAINT BD_MER_DIAGRAMY_BD_OOM_FK1 FOREIGN KEY (OOM_ID)
	  REFERENCES BD_OOM (KOD) ENABLE;
  ALTER TABLE BD_MER_DIAGRAMY ADD CONSTRAINT BD_MER_DIAGRAMY_BD__CISEL_FK1 FOREIGN KEY (JEDNOTKA)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
  ALTER TABLE BD_MER_DIAGRAMY ADD CONSTRAINT BD_MER_DIAGRAMY_BD__CISEL_FK2 FOREIGN KEY (DRUH_MERANIA)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_MER_NEPRIEBEHOVE
--------------------------------------------------------

  ALTER TABLE BD_MER_NEPRIEBEHOVE ADD CONSTRAINT BD_MER_NEPRIEBEHOVE_BD_OO_FK1 FOREIGN KEY (OOM_ID)
	  REFERENCES BD_OOM (KOD) ENABLE;
  ALTER TABLE BD_MER_NEPRIEBEHOVE ADD CONSTRAINT BD_MER_NEPRIEBEHOVE_BD__C_FK1 FOREIGN KEY (DRUH_MERANIA)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
  ALTER TABLE BD_MER_NEPRIEBEHOVE ADD CONSTRAINT BD_MER_NEPRIEBEHOVE_BD__C_FK2 FOREIGN KEY (TYP_ODPOCTU)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_OOM
--------------------------------------------------------

  ALTER TABLE BD_OOM ADD CONSTRAINT BD_OOM_BD_SUSTAVY_FK1 FOREIGN KEY (SUSTAVA_ID)
	  REFERENCES BD_SUSTAVY (KOD) ENABLE;
  ALTER TABLE BD_OOM ADD CONSTRAINT BD_OOM_BD__CISELNIKY_FK1 FOREIGN KEY (DRUH)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
  ALTER TABLE BD_OOM ADD CONSTRAINT BD_OOM_BD__CISELNIKY_FK2 FOREIGN KEY (TYP_MERANIA)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
  ALTER TABLE BD_OOM ADD CONSTRAINT BD_OOM_BD__CISELNIKY_FK3 FOREIGN KEY (NAPATOVA_UROVEN)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_SUSTAVY
--------------------------------------------------------

  ALTER TABLE BD_SUSTAVY ADD CONSTRAINT BD_SUSTAVY_BD__CISELNIKY_FK1 FOREIGN KEY (TYP_SUSTAVY)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_TDO_NORM
--------------------------------------------------------

  ALTER TABLE BD_TDO_NORM ADD CONSTRAINT BD_TDO_NORM_BD__CISELNIKY_FK1 FOREIGN KEY (TYP_TDO)
	  REFERENCES BD__CISELNIKY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_TDO_NORM_CASOVE_RADY
--------------------------------------------------------

  ALTER TABLE BD_TDO_NORM_CASOVE_RADY ADD CONSTRAINT BD_TDO_NORM_CASOVE_RADY_B_FK1 FOREIGN KEY (DIAGRAM_ID)
	  REFERENCES BD_TDO_NORM_DIAGRAMY (KOD) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table BD_TDO_NORM_DIAGRAMY
--------------------------------------------------------

  ALTER TABLE BD_TDO_NORM_DIAGRAMY ADD CONSTRAINT BD_TDO_NORM_DIAGRAMY_BD_T_FK1 FOREIGN KEY (NORM_TDO_ID)
	  REFERENCES BD_TDO_NORM (KOD) ENABLE;
