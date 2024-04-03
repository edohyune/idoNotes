- VMware 준비
    
    [[VMware Disk Size 수정 후 조치내용]]
    
    > [!info] ChatGPT  
    > VMware에서 디스크 용량을 100G로 설정한 후, Ubuntu에서 해당 디스크 용량 전체를 인식하지 않는 경우는, 추가된 공간을 인식하고 사용할 수 있도록 파티션 및 파일 시스템을 조정해야 합니다.  
    > [https://chat.openai.com/share/0e52324f-81c0-426c-8817-64d0a91076e0](https://chat.openai.com/share/0e52324f-81c0-426c-8817-64d0a91076e0)  
    
- Odoo (/opt/odoo/)
    - Install
        - ==Packaged Installers== `Faile`
            
            [[How To Install Odoo 16 on Ubuntu 22.04]]
            
            - PrePare
                
                [[Preparing the odoo]]
                
                  
                
            - Repository
                
                Odoo의 공식 저장소를 시스템에 추가하고, 그 저장소로부터 Odoo를 설치하는 작업을 수행
                
                ```Bash
                wget -q -O - https://nightly.odoo.com/odoo.key | sudo gpg --dearmor -o /usr/share/keyrings/odoo-archive-keyring.gpg
                
                echo 'deb [signed-by=/usr/share/keyrings/odoo-archive-keyring.gpg] https://nightly.odoo.com/master/nightly/deb/ ./' | sudo tee /etc/apt/sources.list.d/odoo.list
                
                sudo apt-get update && sudo apt-get install odoo
                ```
                
            - Distribution package
                
                **==아래 부분을 이해하지 못해서 실패!!==**
                
                ![[ZZ_Files/Untitled 14.png|Untitled 14.png]]
                
        - ==Source Install== `Success`> [!important]  
            > 초기 사용자 아이디로 데이터 베이스 생성  
            - Git을 이용해서 Source Download
                - 다운로드 이전에 설치 위치로 이동
                    1. **개발 환경**: 개발자로서 Odoo를 커스터마이징하거나 개발하려는 경우, 일반적으로 홈 디렉터리의 하위 폴더에 저장하는 것이 좋습니다. 예를 들면 `**~/odoo-dev/**` 디렉터리를 만들어서 그 안에 소스를 저장합니다.
                        
                        ```Shell
                        git clone https://github.com/odoo/odoo.git odoo-dev
                        ```
                        
                    2. **운영 환경**: 실제 운영 환경에서 Odoo를 실행하려는 경우, `**/opt/**` 디렉터리 아래에 저장하는 것이 일반적입니다. `**/opt/**` 디렉터리는 선택적인 응용 프로그램 패키지 소프트웨어를 위한 것입니다.
                        
                        ```Shell
                        sudo git clone https://github.com/odoo/odoo.git /opt/odoo
                        
                        ```
                        
                    3. 테**스트 환경**: 테스트 환경을 설정하는 경우, `**/srv/**` 디렉터리나 홈 디렉터리의 하위 디렉터리를 사용하는 것도 좋은 선택입니다.
                        
                        ```Bash
                        git clone https://github.com/odoo/odoo.git /opt/odoo
                        or
                        git clone git@github.com:odoo/odoo.git /opt/odoo
                        ```
                        
            - Preparation
                
                [[Preparing the odoo]]
                
                [[Preparing the odoo]]
                
                - Dependencies
                    
                    pip을 이용해서 종속성, 의존성 확인
                    
                    ```Bash
                    cd /opt/odoo
                    sed -n -e '/^Depends:/,/^Pre/ s/ python3-\(.*\),/python3-\1/p' debian/control | sudo xargs apt-get install -y
                    
                    >>오른쪽에서 왼쪽으로 읽기의 언어
                    sudo npm install -g rtlcss
                    sudo apt install nodejs npm >>npm 설치가 안되어있을경우
                    ```
                    
            - Exception handling
                
                > DB 초기화
                
                ```Bash
                ./odoo-bin -i base -d iadmin --addons-path=addons
                ```
                
            - ETC
                
                ```Bash
                lsb_release -a && ip r
                
                sudo apt install -y git wget nodejs npm python3 build-essential libzip-dev python3-dev libxslt1-dev python3-pip libldap2-dev python3-wheel libsasl2-dev python3-venv python3-setuptools node-less libjpeg-dev xfonts-75dpi xfonts-base libpq-dev libffi-dev fontconfig
                wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6.1-2/wkhtmltox_0.12.6.1-2.jammy_amd64.deb
                sudo dpkg -i wkhtmltox_0.12.6.1-2.jammy_amd64.deb
                ```
                
            - Start / Stop / Restart
                - Running Odoo (start, stop)
                    
                    Manual
                    
                    ```Shell
                    python3 /opt/odoo/odoo-bin --addons-path=/opt/odoo/addons -d iadmin
                    ```
                    
                    Auto 등록
                    
                    ```JavaScript
                    sudo vim /etc/systemd/system/odoo-auto.service
                    ```
                    
                    ```JavaScript
                    [Unit]
                    Description=Odoo Auto Start
                    After=postgresql.service
                    wants=postgresql.service
                    
                    [Service]
                    User=iadmin
                    Group=iadmin
                    ExecStart=/usr/bin/python3 /opt/odoo/odoo-bin --addons-path=/opt/odoo/addons -d iadmin
                    Restart=always
                    
                    [Install]
                    WantedBy=multi-user.target
                    ```
                    
                    ```JavaScript
                    sudo systemctl daemon-reload
                    ```
                    
                    등록 이후에는
                    
                    ```JavaScript
                    sudo systemctl start odoo-auto.service
                    sudo systemctl stop odoo-auto.service
                    ```
                    
        - Docker
            
            > Install Docker
            
            > **Start a PostgreSQL server**
            
            ```Bash
            docker run -d -e POSTGRES_USER=odoo -e POSTGRES_PASSWORD=odoo -e POSTGRES_DB=postgres --name db postgres:15
            ```
            
    - 프로그램 사용방법 숙지
        - 데이터 등록 방법 숙지
    - addons App 등록
        - [https://apps.odoo.com/apps](https://apps.odoo.com/apps) 설치
    - Share Data
        - API Apps를 확인 또는 기본 App에서 확인
- PostgreSQL
    
    - Server 설치 (On Ubuntu)
        - odoo Source 설치
            
            ```Bash
            sudo apt-get update
            sudo apt-get upgrade
            
            sudo apt install postgresql postgresql-contrib postgresql-client
            postgresql-contrib : 확장
            
            sudo -u postgres createuser -s ipostgre
            sudo -u postgres createdb ipostgre
            -u user
            -s superuser
            
            PostgreSQL 데이터베이스에 연결하는 데 문제가 발생
            FATAL: role "iadmin" does not exist라는 오류가 발생
            Odoo는 실행자의 아이디로 PostgreSQL에 접근… 
            
            ```
            
        - odoo Package 설치
        - odoo Docker 설치
            
              
            
        - Ubuntu Clean 설치
            
            ```Bash
            sudo apt install postgresql
            >>PostgreSQL의 추가 모듈 및 확장 기능을 포함한 패키지
            sudo apt install postgresql-contrib
            >>PostgreSQL 데이터베이스 서버에 연결하고 상호 작용하기 위한 명령줄 도구(psql)
            sudo apt install postgresql-clientpsq
            ```
            
        - Ubuntu 설치 확인
            
            ```Bash
            pg_config --version
            psql --version
            
            systemctl status postgresql
            dpkg -l | grep postgresql
            ```
            
            - 로그 확인하는방법 확인
            
            ```Bash
            sudo apt install postgresql -y
            ```
            
        - postgreSQL 서버 설정
            
            ```Bash
            >>버젼확인
            psql --version
            ls /etc/postgresql/
            ```
            
            - Allow tcp connection on localhost
                
                in /etc/postgresql/[version]/main/pg_hba.conf
                
                ```Bash
                sudo vim /etc/postgresql/14/main/postgresql.conf
                ```
                
                ```Plain
                listen_addresses = 'localhost,192.168.1.2'
                port = 5432
                max_connections = 80
                ```
                
            - Allow tcp connection from 192.168.1.x network
                
                in /etc/postgresql/<YOUR POSTGRESQL VERSION>/main/postgresql.conf
                
                ```Shell
                sudo vim /etc/postgresql/14/main/pg_hba.conf
                ```
                
                ```Plain
                host all all 192.168.221.1/24 trust --postgresql의 패스워드가 없는 경우
                host all all 192.168.221.1/24 md5
                
                host    all             all             127.0.0.1/32            md5
                host    all             all             192.168.1.0/24          md5
                ```
                
    - Client 설치 - pgAdmin, pgAgent
        
        > [!info] pgAdmin - PostgreSQL Tools  
        > pgAdmin - PostgreSQL Tools for Windows, Mac, Linux and the Web  
        > [https://www.pgadmin.org/](https://www.pgadmin.org/)  
        
        **PSQL을 이용하여 PostgreSQL 서버에 연결**
        
        ```Bash
        sudo -u postgres psql\dt
        >>
        psql -U postgres
        >>
        psql -U username -d databasename
        >>
        psql -h hostname -p port -U username -d databasename
        ```
        
        - psql에서 유용한 명령어:
            
            #### Key Word
            
            |이름|설명|구분|
            |---|---|---|
            |[[list]]|모든 데이터베이스 목록 표시.|psql|
            |[[timing]]|쿼리 실행 시간을 표시/숨기기.|psql|
            |[[x]]|결과의 출력 형식을 확장된 형식으로 전환. 이는 행과 열이 많은 쿼리 결과에 유용합니다.|psql|
            |[[e]]|마지막으로 실행한 쿼리를 텍스트 편집기에서 열기. 수정 후 저장하고 닫으면 수정된 쿼리가 실행됩니다.|psql|
            |[[q]]|psql 세션 종료.|psql|
            |[[i]]|[filename.sql]: SQL 파일 실행.|psql|
            |[[d]]|[tablename]: 특정 테이블의 구조를 보여줍니다.|psql|
            |[[dn]]|모든 스키마를 나열.|psql|
            |[[du]]|모든 PostgreSQL 사용자 계정을 나열.|psql|
            |[[dt]]|현재 데이터베이스의 모든 테이블을 나열.|psql|
            |[[c]]|[databasename]: 다른 데이터베이스로 전환.|psql|
            |[[l]]|모든 데이터베이스 목록 표시.|psql|
            
              
              
            
    - User
        - postgreSQL 사용자의 종류와 사용자 추가하기
    - Create Database
        - database 생성 및 삭제
        - Table, View 생성 및 삭제
        - 프로시저 , 테이블 함수, 스칼라 함수 생성 및 삭제
    - Share Data
        - MsSQL과 연동방법 확인
    - Start
        
        ```Bash
        sudo -i -u postgres[userid]
        ```
        
    
      
    
- Python
    
    ```Bash
    python3 --version
    pip3 --version
    
    sudo apt-get purge --auto-remove python3
    sudo apt-get purge --auto-remove python3-pip
    sudo apt-get update
    sudo apt-get upgrade
    sudo apt-get install python3
    sudo apt-get install python3-pip
    
    >> 추가설정 : Python 및 pip의 기본 명령어를 python 및 pip로 설정
    sudo ln -s /usr/bin/python3 /usr/bin/python
    sudo ln -s /usr/bin/pip3 /usr/bin/pip
    
    *주의: 기존에 설치된 Python 및 pip을 제거하는 것은 다른 시스템 프로그램이나 의존성에 영향을 줄 수 있습니다. 이러한 작업을 수행하기 전에 백업 및 검토를 권장합니다.
    ```
    
    - Database CRUD
    - Shard Data
        - API GET, POST
    
      
    
- Ubuntu
    - Operation
        
        #### Key Word
        
        |이름|설명|구분|
        |---|---|---|
        |[[sudo]]|superuser do update|Linux Operations|
        |[[apt]]|Advanced Package Tool|Linux Operations|
        
          
          
        
    - Install
        
        > [!info] Ubuntu Server 22.04 LTS Install - Step by Step Guide - (Beginners Tutorial and Bonus! Web Server)  
        > Ubuntu Server 22.  
        > [https://www.youtube.com/watch?v=zs2zdVPwZ7E&t=668s](https://www.youtube.com/watch?v=zs2zdVPwZ7E&t=668s)  
        
        파일시스템 설정 Default 값으로
        
        ![[ZZ_Files/Untitled 1 6.png|Untitled 1 6.png]]
        
        사용자 설정
        
        ![[ZZ_Files/Untitled 2 4.png|Untitled 2 4.png]]
        
        Server information trnsvr iadmin/iadmin
        
          
        
        update 확인, upgrade : 확인된 update를 설치
        
        ```Bash
        sudo apt update
        sudo apt upgrade
        ```
        
        [[SSH]]
        
        [[네트워크 상태 확인]]
        
    - User
        - List
            - 사용자 정보 및 Role을 읽어오는 방법
        - Add
            - 사용자 추가
            - 사용자의 Role 추가
        - Remove
            - 사용자 삭제
    - Network
        - [x] 아이피 정보를 읽어오는 방법
            
            ip addr
            
        - 고정아이피 셋팅하는 방법
        - DHCP로 변경하는 방법
    - Download
        
        - File Download
            - Web Data Download
            - Archive Control
        - Text Edit
            - Vim install
            - 다른 파일의 데이터를 복사하기
            - Notion 또는 웹상의 Text에 접근해서 복사해서 명령어로 사용하기
        
          
        
          
        
    - SSH
        
        - SSH 인증 이용하기
            - OpenSSH 설치
            - Server SSH-KEY
        
        - Client SSH-KEY 생성

  

- Odoo Source 설치 (/opt/odoo/)
    
    ```Shell
    [Package Download]
    sudo git clone https://github.com/odoo/odoo.git /opt/odoo/
    
    [pip Install]
    python --version
    python3 --version
    pip3 --version
    sudo apt update
    sudo apt upgrade
    sudo apt install python3-pip
    sudo apt-get install python3-pip
    
    [추가설정 : Python 및 pip의 기본 명령어를 python 및 pip로 설정]
    sudo ln -s /usr/bin/python3 /usr/bin/python
    sudo ln -s /usr/bin/pip3 /usr/bin/pip
    
    [nodejs]
    sudo apt update
    sudo apt upgrade
    sudo apt install nodejs npm
    sudo npm install -g rtlcss
    
    [Dependencies(종속성)]
    cd /opt/odoo/
    sed -n -e '/^Depends:/,/^Pre/ s/ python3-\(.*\),/python3-\1/p' debian/control | sudo xargs apt-get install -y
    또는 
    sudo apt install python3-pip libldap2-dev libpq-dev libsasl2-dev
    cd /opt/odoo/
    pip install -r requirements.txt
    
    [PostgreSQL Install]
    sudo apt install postgresql postgresql-client
    
    sudo -u postgres createuser -s iadmin
    sudo -u postgres createdb iadmin
    
    [Odoo Database 초기화]
    ./odoo-bin -i base -d iadmin --addons-path=addons
    
    [odoo 실행]
    python3 /opt/odoo/odoo-bin --addons-path=/opt/odoo/addons -d iadmin
    
    [odoo 자동 실행 등록]
    sudo vim /etc/systemd/system/odoo-auto.service
    
    
    ==============================================================================
    [Unit]
    Description=Odoo Auto Start
    After=postgresql.service
    wants=postgresql.service
    
    [Service]
    User=iadmin
    Group=iadmin
    ExecStart=/usr/bin/python3 /opt/odoo/odoo-bin --addons-path=/opt/odoo/addons -d iadmin
    Restart=always
    
    [Install]
    WantedBy=multi-user.target
    ==============================================================================
    
    sudo systemctl daemon-reload
    
    sudo systemctl start odoo-auto.service
    sudo systemctl stop odoo-auto.service
    ```
    
    - PostgreSql Server 연결
        
        ```Shell
        사용자 postgres패스워드 설정이 안되어 있으면 접속이 안된다
        서버 접속을 위해서는 우선 Password 수정을 해주는 것이 좋다.
        [PostgreSql 접속]
        sudo -u postgres psql
        ```
        
        ![[ZZ_Files/Untitled 3 4.png|Untitled 3 4.png]]
        
        ```Shell
        sudo vim /etc/postgresql/14/main/postgresql.conf
        [접속가능 IP셋팅]
        listen_address = '*'
        또는 
        listen_address = 'localhost,192.168.88.1'
        로 수정한다. 
        
        sudo vim /etc/postgresql/14/main/pg_hba.conf
        [접속가능 IP4 추가]
        IPv4에 host all all 0.0.0.0/0 md5 를 추가한다.
        
        [방화벽확인] - 방화벽이 셋팅이 되면 8096, 20, 등 사용하는 모든 포트를 열어줘야한다. 
        sudo ufw status | grep 5432
        sudo ufw allow 5432/tcp 
        ```
        
    - pgAdmin Settin
        
        > [!info] PostgreSQL: File Browser  
        > Copyright © 1996-2023 The PostgreSQL Global Development Group  
        > [https://www.postgresql.org/ftp/pgadmin/pgadmin4/v8.0/windows/](https://www.postgresql.org/ftp/pgadmin/pgadmin4/v8.0/windows/)  
        
        ![[ZZ_Files/Untitled 4 2.png|Untitled 4 2.png]]
        
    
    [[postgreSQL TABLE check]]
    

  

- Debug Page 들어가는 방법
    1. 주소 web뒤에 ==?debug=1== 추가
        
        https://erp.kokkok.asia/web==?debug=1==\#id=462&cids=1&menu_id=109&action=168&……
        
        ![[ZZ_Files/Untitled 5 2.png|Untitled 5 2.png]]
        
    2. Setting에서 선택
        
        ![[ZZ_Files/Untitled 6 2.png|Untitled 6 2.png]]
        

  

  

[[LANGUAGE 'plpgsql']]

[[Ubunto 64-bit 작업일지]]