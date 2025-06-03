# HC4U 2.0 - Der einfache Chat-Client 💬

Willkommen bei HC4U 2.0, einem unkomplizierten Chat-Client, der auf Benutzerfreundlichkeit und grundlegende Chat-Funktionalitäten setzt. Egal, ob du einen Server hosten oder einem bestehenden Chat beitreten möchtest, HC4U 2.0 macht es dir leicht, mit Freunden und Kollegen in Verbindung zu bleiben!

## ✨ Funktionen

* **Server hosten**: Starte deinen eigenen Chat-Server mit einem Klick und lasse andere beitreten.
* **Chat beitreten**: Verbinde dich ganz einfach mit einem bestehenden Server, indem du die Server-IP-Adresse eingibst.
* **Nachrichten senden und empfangen**: Tausche Nachrichten in Echtzeit mit anderen verbundenen Benutzern aus.
* **Automatische IP-Erkennung**: Der Server versucht, deine lokale IP-Adresse zu erkennen und anzuzeigen, um das Beitreten für andere zu erleichtern.
* **Benutzerfreundliche Oberfläche**: Eine einfache und intuitive Windows Forms-Oberfläche für reibungslose Kommunikation.
* **Hintergrund-Threads**: Server- und Client-Operationen laufen in separaten Threads, um die Benutzeroberfläche reaktionsschnell zu halten.
* **Einfache Eingabe**: Sende Nachrichten bequem durch Drücken der Enter-Taste.

## 🏗️ Technische Übersicht und Projekt-Aufbau

Dieses Projekt ist eine C#-Windows Forms-Anwendung, die grundlegende Netzwerkfunktionen für die Chat-Kommunikation nutzt.

### 🌐 Architektur

Der Chat-Client basiert auf einem einfachen Client-Server-Modell unter Verwendung von `TcpListener` und `TcpClient`.

* **Server-Seite**:
    * Der Server wird mit `TcpListener` gestartet und lauscht auf eingehende Verbindungen auf Port 5000.
    * Jeder neu verbundene Client wird in einer Liste (`clients`) gespeichert.
    * Für jeden Client wird ein separater Thread gestartet, um Nachrichten asynchron zu empfangen (`ReceiveMessages`).
    * Nachrichten, die von einem Client empfangen werden, werden an alle anderen verbundenen Clients weitergeleitet (`BroadcastMessage`).
    * Die lokale IP-Adresse des Servers wird erkannt und dem Benutzer angezeigt.

* **Client-Seite**:
    * Ein Client stellt eine Verbindung zu einem Server unter Verwendung von `TcpClient` her, wobei eine IP-Adresse und Port 5000 verwendet werden.
    * Nachrichten werden über einen `NetworkStream` gesendet und empfangen.
    * Das Senden von Nachrichten (`SendMessage`) erfolgt über den `NetworkStream` des Clients oder durch Weiterleitung an den Server (`BroadcastMessage`) bei einem gehosteten Server.
    * Empfangene Nachrichten werden im Chat-Fenster angezeigt.

### 📂 Projekt-Struktur

Das Projekt ist in einem typischen C#-Windows Forms-Layout organisiert:

```
HC4Uv2/
├── .vs/                                    # Visual Studio Konfigurationsdateien
│   ├── HC4U 2.0/
│   │   └── v17/
│   │       ├── DocumentLayout.backup.json  # Backup des Dokument-Layouts
│   │       ├── DocumentLayout.json         # Aktuelles Dokument-Layout
│   │       └── ResourceExplorer/
│   │           └── settings.json           # Einstellungen des Ressourcen-Explorers
│   └── HC4Uv2/
│       └── v17/
│           └── DocumentLayout.json         # Dokument-Layout für das Hauptprojekt
├── HC4U 2.0/                               # Hauptprojektordner der Anwendung
│   ├── bin/                                # Kompilierte Binärdateien
│   │   └── Debug/                          # Debug-Build-Ausgabe
│   │       └── net8.0-windows/
│   │           ├── HC4U 2.0.deps.json      # Abhängigkeiten der Anwendung
│   │           └── HC4U 2.0.runtimeconfig.json # Laufzeitkonfiguration
│   ├── obj/                                # Objektdateien und temporäre Build-Artefakte
│   │   └── Debug/
│   │       └── net8.0-windows/
│   │           ├── .NETCoreApp,Version=v8.0.AssemblyAttributes.cs # Assembly-Attribute
│   │           ├── HC4U 2.0.AssemblyInfo.cs # Assembly-Informationen
│   │           ├── HC4U 2.0.GlobalUsings.g.cs # Globale Using-Anweisungen
│   │           ├── HC4U 2.0.csproj.FileListAbsolute.txt # Liste der Projektdateien
│   │           ├── HC4U 2.0.designer.deps.json # Designer-Abhängigkeiten
│   │           ├── HC4U 2.0.designer.runtimeconfig.json # Designer-Laufzeitkonfiguration
│   │           └── HC4U 2.0.sourcelink.json # Sourcelink-Informationen
│   ├── Form1.cs                            # Hauptlogik des Chat-Clients (ChatForm-Klasse)
│   ├── Form1.Designer.cs                   # Designer-generierter Code für die Benutzeroberfläche
│   ├── Program.cs                          # Startpunkt der Anwendung, enthält Git-Update-Logik
│   └── HC4U 2.0.csproj.nuget.dgspec.json   # NuGet-Paket-Informationen
└── VSWorkspaceState.json                   # Visual Studio Workspace-Status
```

### 🛠️ Verwendete Technologien

* **C#**: Die Hauptprogrammiersprache.
* **.NET 8.0**: Das Framework, auf dem die Anwendung basiert.
* **Windows Forms**: Für die grafische Benutzeroberfläche.
* **System.Net.Sockets**: Für die Netzwerkkommunikation (TCP/IP).
* **Git**: Für die Versionskontrolle und automatische Aktualisierungen des Repositorys.

## 🚀 Erste Schritte

###  Voraussetzungen

* .NET 8.0 Runtime installiert
* Git installiert (falls du die automatische Update-Funktion nutzen möchtest)

### Ausführen der Anwendung

1.  **Klone das Repository**:
    ```bash
    git clone https://github.com/TristanAtze/HC4Uv2.git
    ```
    *(Hinweis: Die `Program.cs` enthält bereits eine Logik zum Klonen/Aktualisieren des Repositories, aber dies ist der manuelle Weg)*.

2.  **Öffne das Projekt in Visual Studio**.

3.  **Starte die Anwendung** (F5 oder Debuggen -> Debugging starten).

### Nutzung der Chat-Anwendung

1.  **Server Hosten**: Klicke auf den Button "Server Hosten". Dir wird deine lokale IP-Adresse angezeigt. Teile diese IP-Adresse mit anderen, die beitreten möchten.
2.  **Chat Beitreten**: Klicke auf den Button "Chat Beitreten" und gib die IP-Adresse des Servers ein, dem du beitreten möchtest.
3.  **Nachrichten senden**: Gib deine Nachricht in das untere Textfeld ein und drücke Enter.

---
