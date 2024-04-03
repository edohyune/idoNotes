- Miscellaneous
    
    Users and Companies
    
    **Add individual users**
    
    User : 일상적인 작업을 수행하기 위해 데이터베이스에 액세스할 수 있는 사람으로 정의( 액세스 권한 설정 가능)
    
    Internal User
    
    Portal
    
    Public
    
      
    
    ![[manage-users.png]]
    
    ![[new_user.png]]
    
    이메일이 자동으로 사용자에게 전송됩니다. 사용자는 초대를 수락하고 로그인을 생성
    
    ![[password-reset-login.png]]
    
      
    

  

- Research
    
    - 개요 : MS SQL과 PostgreSQL 테이블 데이터 공유
        - 데이터 마이그레이션 도구 사용:  
            pgloader: pgloader는 데이터를 PostgreSQL로 마이그레이션하는 도구입니다. MSSQL에서 데이터를 추출하고 PostgreSQL에 적재할 수 있습니다.  
            DMS (Data Migration Services): 클라우드 제공 업체들은 종종 이런 서비스를 제공하며, 예를 들면 AWS의 DMS는 다양한 소스 데이터베이스에서 PostgreSQL로의 마이그레이션을 지원합니다.  
            
        - 외부 테이블(Foreign Data Wrapper), Linked Server(ODBC):  
            PostgreSQL의 foreign data wrapper를 사용하여 MS SQL 테이블을 외부 테이블로 취급하면 PostgreSQL 내에서 MS SQL 데이터에 직접 액세스할 수 있습니다.  
            postgresql_fdw: 이 Foreign Data Wrapper를 사용하여 PostgreSQL 인스턴스 간에 데이터를 공유할 수 있습니다.  
            tds_fdw: 이 Foreign Data Wrapper를 사용하여 PostgreSQL에서 MS SQL Server 데이터에 액세스할 수 있습니다.  
            
        - 데이터 덤프 및 복원:  
            MS SQL에서 데이터를 덤프하여 SQL 파일로 내보낸 다음 PostgreSQL에서 이 파일을 가져올 수 있습니다. 그러나 SQL 문법의 차이로 인해 일부 수정이 필요할 수 있습니다.  
            
        - ETL 도구 사용:  
            Talend, Apache NiFi, Apache Kafka 등의 ETL 도구를 사용하여 데이터를 추출, 변환 및 적재하는 프로세스를 자동화할 수 있습니다. 이 도구들은 MSSQL과 PostgreSQL 사이의 데이터 전송을 지원합니다.  
            
        - 직접 프로그래밍(API):  
            Python, Java, C# 등의 언어로 스크립트나 프로그램을 작성하여 MSSQL에서 데이터를 가져와 PostgreSQL에 삽입하는 것입니다. pymssql와 psycopg2와 같은 라이브러리를 사용하여 Python에서 이를 수행할 수 있습니다.  
            
    
		ERP → ODOO PULL
		ERP → ODOO PUSH
		ODOO → ERP PULL
		ODOO → ERP PUSH

Odoo에서 회계처리 이용하기

Odoo Ent 버젼의 라이센스 비용 회비 방법

별도의 사용자 인풋 폼을 만든다.