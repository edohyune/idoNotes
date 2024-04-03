[[개발환경 설치]]

- Django를 활용한 쉽고 빠른 웹 개발 - Python Web Programing
    - Chapter01
        
        Django 쉽고 빠르다.
        
        단축함수, 제네릭 뷰, 서드파티 라이브러리, 다양한 레퍼런스
        
        1.1 Django 개발의 기본 사항
        
        1.1.1 MVT Model, View, Template
        
        startproject, startapp을 이용하여 기본 디렉토리와 파일을 생성
        
        Model : models.py
        
        테이블을 정의, Object Relational Mapping 사용
        
        Class - django.db.models.Model 상속받아 사용
        
        테이블의 신규, 변경 발생할때 마이그레이션 발생
        
        migrations/ 디렉토리에 변경 사랑을 실제 데이터베이스에 반영하는 makemigrations migrate명령을 제공
        
        View : views.py
        
        함수형 뷰,
        
        클래스형 뷰 - 재사용 및 확장성 제네릭 뷰를 사용가능.
        
        Template : templeates 하위 *.html에 작성
        
        base.html 등 Look and Feel에 관련된 파일도 있다.
        
        프로젝트 베이스 디렉터리 : /home/shkim/pyDjango/ch99/
        
        프로젝트 디렉터리 : /home/shkim/pyDjango/ch99/mysite/
        
        프로젝트 템플릿 디렉터리 : /home/shkim/pyDjango/ch99/templates/
        
        앱 템플릿 디렉터리 :
        
        템플릿 파일을 찾는 순서 - 프로젝트 템플릿 디렉터리 → 앱 템플릿 디렉터리
        
        INSTALED_APPS설정항록에 등록된 순서
        
        ETC. URLconf - URL과 뷰(함수 또는 클래스)를 맵핑해주는 urls.py
        
        urls.py는 프로젝트 URL과 앱 URL을 분리할 수 있다. namespace를 지정하여 패턴그룹별로 관리가 가능하다.
        
        1.1.2 MVT 코딩 순서
        
        테이블설계 : Model
        
        화면설계 : View, Template
        
        UI를 설계하면서 비지니스 로직을 구현할때는 Template 우선으로 하나 클래스형(?) 뷰와 같이 간단한 뷰는 View를 우선으로 한다.
        
        ```Plain
        프로젝트 뼈대 만들기
        Model Coding : models.py, admin.py
        URLconf Coding : URL 및 매핑관계를 정의(urls.py)
        View Coding : Business Logic (views.py)
        Template Coding : UI 개발 
        ```
        
        1.1.3 Settings.py
        
        Project configuration file
        
        ```Plain
        Database : Default - SQLLite3
        Applications registration
        Templates 항목으로 지정
        Static_URL등 관련 항목을 지정
        TimeZone 지정
        ```
        
        1.1.8 Admin Site
        
        테이블의 내용을 열람하고 수정하는 기능을 제공
        
        User와 Group을 기본 제공, 추가 테이블에 대해서는 admin.py에서 작업
        
          
        
        1.2 가상환경
        
        virtualenv 툴 : 독립된 가상환경을 구성
        
        외부라이브러리의 사용이 필연적이고 외부라이브러리는 동일한 버전에 구성이 어렵다.