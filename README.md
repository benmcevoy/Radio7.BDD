Radio7.BDD
==========

Selenium and Specflow Base

The tests expect this site to be running at http://localhost:50523.

You may need to set up an IIS site to host this site, in which case update the <strong>baseUrl</strong> in the <strong>app.config</strong> of the <strong>Radio7.BDD.SampleWebsite.Test</strong> project.

<pre><code>
         &lt;seleniumConfig baseUrl="http://localhost:50523/"
                         webDriverType="Chrome"
                         driverDirectory="Drivers\Chrome\2.29" /&gt;
</code></pre>
