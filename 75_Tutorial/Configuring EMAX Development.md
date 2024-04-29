- 소스컨트롤 프로그램 SVN 사용법
- 소스컨트롤 프로그램 GitHub 사용법
- 소스컨트롤 프로그램 TeamFoundation 사용법

### PC Setting

1. Install Windows
2. Install VisualStudio 2017
    
    > Optional : Thema Pack
    > 
    > ![[ZZ_Files/VisualStudioThemePack 2.vsix|VisualStudioThemePack 2.vsix]]
    
3. Install .Net Development Pack 4.6.2
    
    > ndp462-devpack-kb3151934-enu.exe
    
4. Install DevExpress 18.2
    
    > When install (Must to select “Trail Installation Version “)
    
5. Install TortoiseSVN-1.14.5.29465-x64-svn-1.14.2
    
    > Can Restart or Do not restart pc
    
    - SVN Setting (Do Not Commit obj, debug, release)
        
        ![[Untitled 18.png|Untitled 18.png]]
        
        TortoiseSVN(Right Click) > Settings
        
        General / Subversion / Edit Button
        
        ```Plain
        global-ignores = Thumbs.db *.map *.exe *.res mt.dep BuildLog.htm *.ilk *.exe.embed.manifest *.exe.intermediate.manifest *.obj *.pch *.pdb *.idb *.user *.aps *.ncb *.suo *.o *.lo *.la *.al .libs *.so *.so.[0-9]* *.a *.pyc *.pyo *.rej *~ #*# .#* .*.swp .DS_Store
        #   *.rej *~ #*# .#* .*.swp .DS_Store [Tt]humbs.db
        ```
        
        When the config file is opened, find global-ignores, change the sentence containing # to the above sentence, and save.
        
6. CheckOut Source Code
7. Create Solution
8. Set Local Build Directory : C:\inetpub\wwwroot\System9
9. Set and Connect FTP Client
10. Install SSMS
    
    > Tab Setup  
    >   
    > Query ShortCuts  
    > CTRL+0 : BSP_MAKE_CRUD  
    > CTRL+9 : BSP_SELECT_HELP  
    > CTRL+8 : BSP_SP_HELP  
    >   
    > DarkMode  
    > '''C:\Program Files (x86)\Microsoft SQL Server Management Studio 20\Common7\IDE'''
    > ssms.pkgundef  
    
11. Windows Search
    
    > Indexing Options