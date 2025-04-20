# Bu Dockerfile, Visual Studio tarafından hızlı debug için kullanılabilecek şekilde yapılandırılabilir.
# Daha fazla bilgi: https://aka.ms/customizecontainer

# ASP.NET runtime içeren base (temel) imajdan başlıyoruz.
# Bu imaj uygulamayı çalıştırmak için yeterlidir, derleme yapamaz.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Docker içinde uygulamanın çalışacağı klasörü belirliyoruz
WORKDIR /app

# Uygulama dışarıdan 80 (HTTP) ve 443 (HTTPS) portlarıyla erişilebilir olacak
EXPOSE 80
EXPOSE 443

# SDK içeren bir imajla devam ediyoruz (derleme yapabilmek için gerekli)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Kodları yerleştireceğimiz çalışma klasörünü belirliyoruz
WORKDIR /src

# Projenin ana bileşenlerini (katmanları) sırayla imaja kopyalıyoruz (sadece .csproj dosyaları)
# Bu sayede bağımlılıkları önceden yükleyip cache kullanabiliriz
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]

# WebApi projesi üzerinden NuGet paketlerini indiriyoruz
RUN dotnet restore "WebApi/WebApi.csproj"

# Şimdi tüm proje dosyalarını Docker imajına kopyalıyoruz (kodlar, konfigürasyonlar vb.)
COPY . .

# WebApi klasörüne geçiyoruz, çünkü derleyeceğimiz ana proje bu
WORKDIR "/src/WebApi"

# Uygulamayı derliyoruz (yayın modunda) ve çıktıları /app/build klasörüne koyuyoruz
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

# Yayın aşamasına geçiyoruz, bu adımda sadece gerekli dosyalar alınacak
FROM build AS publish

# Uygulamayı yayın modunda publish ediyoruz ve çıktıyı /app/publish klasörüne alıyoruz
# /p:UseAppHost=false → gereksiz .exe dosyası üretimini engeller, sadece DLL üretir
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Şimdi en başta tanımladığımız "çalıştırma için hafif base imaj"a geçiyoruz
FROM base AS final

# Çalışma klasörünü /app olarak ayarlıyoruz
WORKDIR /app

# Yayın aşamasında çıkan çıktıları bu klasöre kopyalıyoruz
COPY --from=publish /app/publish .

# Uygulama çalıştığında çalışacak komutu tanımlıyoruz
ENTRYPOINT ["dotnet", "WebApi.dll"]
