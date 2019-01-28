// JavaScript Document

 $(function() {
        var scntDiv = $('#p_scents');
        var i = $('#p_scents p').size() + 1;
        
        $('#addScnt').live('click', function() {
                $('<p><span class="col-xs-12 col-sm-6 col-md-6 col-lg-6"><label class="input"><select title="Toll Plaza" class="text-area"><option>single select</option></select></label></span><span class="col-xs-12 col-sm-6 col-md-6 col-lg-6"><label class="input"><i class="icon-append fa fa-lock"></i><input type="text" title="Quantity"/><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your Quantity</b></label></span></p>').appendTo(scntDiv);				
                i++;
                return false;
        });
        
        $('#remScnt').live('click', function() { 
                if( i > 2 ) {
                        $(this).parents('p').remove();
                        i--;
                }
                return false;
        });
});

$(function() {
        var scntDiv = $('#p_scents2');
        var i = $('#p_scents2 p').size() + 1;
        
        $('#addScnt').live('click', function() {
                $('<div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"><label class="input input-hours"><input type="text" title="H" placeholder="H" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your H</b></label><label class="input input-hours"><input type="text" title="H" placeholder="H" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your H</b></label><label class="input input-minut"><input type="text" title="M" placeholder="M" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your M</b></label><label class="input input-hours"><input type="text" title="M" placeholder="M" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your M</b></label></div><div class="col-xs-12 col-sm-6 col-md-6 col-lg-6"><label class="input input-hours"><input type="text" title="H" placeholder="H" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your H</b></label><label class="input input-hours"><input type="text" title="H" placeholder="H" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your H</b></label><label class="input input-minut"><input type="text" title="M" placeholder="M" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your M</b></label><label class="input input-hours"><input type="text" title="M" placeholder="M" /><b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Enter your M</b></label></div>').appendTo(scntDiv);				
                i++;
                return false;
        });
        
        $('#remScnt').live('click', function() { 
                if( i > 2 ) {
                        $(this).parents('p').remove();
                        i--;
                }
                return false;
        });
});

