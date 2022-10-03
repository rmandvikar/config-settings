config-settings
===============
Utility classes to access keys in `appSettings` or `configSections` and their overrides.


#### Accessing key's value:

The typical way is cumbersome for `configSection`:

```c#
string value = ConfigurationManager.AppSettings["CustomConfig.Key1"];
string value = ((NameValueCollection)ConfigurationManager.GetSection("CustomConfig"))["Key1"];
```

A key placed in `appSettings` with a naming convention can be accessed as:
```xml
<appSettings>
  <add key="CustomConfig.Key1" value="..."/>
</appSettings>
```
```xml
string value = ConfigSettings.GetByPrefix("CustomConfig")["Key1"];
```

A key placed in `configSection` can be accessed as:
```xml
<CustomConfig>
  <add key="Key1" value="..."/>
</CustomConfig>
```
```xml
string value = ConfigSettings.GetSection("CustomConfig")["Key1"];
```


#### Overriding a key's value:

##### For `appSettings`:
```xml
<!-- App.config or Web.config -->
<appSettings file="AppSettings-override.config">
  <add key="CustomConfig.Key1" value="Value1 (from app settings)"/>
</appSettings>
```
```xml
<!-- AppSettings-override.config -->
<appSettings>
  <add key="CustomConfig.Key1" value="Value1 (from app settings) (override)"/>
</appSettings>
```
Or using `configSource`:
```xml
<!-- App.config or Web.config -->
<appSettings configSource="AppSettings.config" />
```
```xml
<!-- AppSettings.config -->
<appSettings file="AppSettings-override.config">
  <add key="CustomConfig.Key1" value="Value1 (from app settings)"/>
</appSettings>
```
```xml
<!-- AppSettings-override.config -->
<appSettings>
  <add key="CustomConfig.Key1" value="Value1 (from app settings) (override)"/>
</appSettings>
```

##### For `configSection`:
```xml
<!-- App.config or Web.config -->
<configSections>
  <!-- Note the NameValueFileSectionHandler type -->
  <section name="CustomConfig" type="System.Configuration.NameValueFileSectionHandler" />
</configSections>
<CustomConfig file="CustomConfig-override.config">
  <add key="Key1" value="Value1 (from config section)"/>
</CustomConfig>
```
```xml
<!-- CustomConfig-override.config -->
<CustomConfig>
  <add key="Key1" value="Value1 (from config section) (override)"/>
</CustomConfig>
```
Or using `configSource`:
```xml
<!-- App.config or Web.config -->
<configSections>
  <!-- Note the NameValueFileSectionHandler type -->
  <section name="CustomConfig" type="System.Configuration.NameValueFileSectionHandler" />
</configSections>
<CustomConfig configSource="CustomConfig.config" />
```
```xml
<!-- CustomConfig.config -->
<CustomConfig file="CustomConfig-override.config">
  <add key="Key1" value="Value1 (from config section)"/>
</CustomConfig>
```
```xml
<!-- CustomConfig-override.config -->
<CustomConfig>
  <add key="Key1" value="Value1 (from config section) (override)"/>
</CustomConfig>
```


#### Advantages:

- Indexer interface.
- Moving keys between `appSettings` and `configSection` results in small code change.
- Returns `null` as value if key does not exists in `configSection` instead of throwing (similar to `appSettings`).
- Redundant instances are avoided by using an internal dictionary.
- Ability to override settings for `appSettings` or `configSection` using the `file` attribute (by using `NameValueFileSectionHandler`).
- Overrides are useful for DEV, STG, PRD environments. Each environment will have an override file and a build program picks the corresponding file.
