1 - build icin  
	- oldugu dizini vermek icin .
	- bir ust dizine cikmak icin ../
	- iki ust dizine cikmak icin ../../
	build ederken dockerfile icindeki dosyalara erisebilmesi icin bu yolun dogru verilmesi gerekiyor. icinde path neredeyse ona gore vermek lazim
	-f komutu dockerfile in path ini vermek lazim
	-t image adini belirtir tag lemek icin
	docker build -t kubeservice.cmd -f Dockerfile ../../

	detayli loglama icin 
	docker buildx build -t kubeservice.cmd -f Dockerfile ../../ --progress=plain --no-cache
	
2 - docker run


Seçenek	Açıklama
-d	Arka planda çalıştırır. (anlik calisitiginiz konsolu kullanmaz arka planda calismaya baslar. siz konsolde islem yapmaya devam edebilirsiniz)
-it	Etkileşimli terminal başlatır. (kullanici girdisi ve termimal ile kontainer izleme)
-p	Port yönlendirmesi yapar (örn: -p 8080:80). [disaridan kontainer erismek icin gereken port]:[kontainer in icindeki port]
-e	Ortam değişkeni tanımlar.
-v veya --mount	Klasör paylaşımı yapar.
--rm	Konteyner kapandığında otomatik olarak silinir.
--name	Konteynere özel bir isim verir.

docker run -dit --name Kube.Service.Cmd -p 5000:80 kubeservice.cmd

Dockfile icinde EXPOSE edilen port 80 den farkli oldugu zaman environment olarak url belirtmek gerekiyor, 

ENV ASPNETCORE_URLS=http://+:5000

------------------
kubectl komutlarini kullanmadan once mutlaka docker login olmaniz gerekiyor, yoksa image cekmede sorun olabilir.
docker login docker.io
kubectl apply -f deployment.yml
kubectl apply -f service.yml

service yaml daki targetPort dockerfile icindeki expose edilen porta karsilik geliyor, 

targetPort: 5000


deployment yaml daki port - container ifadesi kubernetes in network unde bu pod a ulasmak icin kullanacagi port anlamina geliyor,

ports:
- containerPort: 80

service.yaml daki karsili port degiskeni oluyor

port: 80

service yaml daki nodePort port ise disaridan pod a erismek icin gereken port

nodePort: 32500